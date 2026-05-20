using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Ventas;
using MediTrack.Aplicacion.Interfaces;
using MediTrack.Dominio.Entidades;
using MediTrack.Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Aplicacion.Servicios
{
    public class ServicioVentas : IServicioVentas
    {
        private readonly ContextoAplicacion _contexto;

        public ServicioVentas(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DtoVenta>> ObtenerTodas()
        {
            var ventas = await _contexto.Ventas
                .Include(v => v.Paciente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Medicamento)
                .Where(v => v.Activo)
                .OrderByDescending(v => v.FechaVenta)
                .ToListAsync();

            return ventas.Select(v => MapearDto(v)).ToList();
        }

        public async Task<DtoVenta> ObtenerPorId(int id)
        {
            var venta = await _contexto.Ventas
                .Include(v => v.Paciente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Medicamento)
                .FirstOrDefaultAsync(v => v.Id == id && v.Activo);

            if (venta == null)
                throw new Exception("Venta no encontrada.");

            return MapearDto(venta);
        }

        public async Task<List<DtoVenta>> ObtenerPorPaciente(int pacienteId)
        {
            var existePaciente = await _contexto.Usuarios
                .AnyAsync(u => u.Id == pacienteId && u.Activo);

            if (!existePaciente)
                throw new Exception("Paciente no encontrado.");

            var ventas = await _contexto.Ventas
                .Include(v => v.Paciente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Medicamento)
                .Where(v => v.PacienteId == pacienteId && v.Activo)
                .OrderByDescending(v => v.FechaVenta)
                .ToListAsync();

            return ventas.Select(v => MapearDto(v)).ToList();
        }

        public async Task<DtoVenta> Crear(DtoCrearVenta dto)
        {
            // Verificar paciente
            var paciente = await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.Id == dto.PacienteId && u.Activo);

            if (paciente == null)
                throw new Exception("Paciente no encontrado.");

            // Verificar tipo de comprobante
            var tiposValidos = new[] { "Boleta", "Factura" };
            if (!tiposValidos.Contains(dto.TipoComprobante))
                throw new Exception("Tipo de comprobante inválido. Use Boleta o Factura.");

            // Verificar que haya detalles
            if (dto.Detalles == null || dto.Detalles.Count == 0)
                throw new Exception("La venta debe tener al menos un medicamento.");

            // Verificar stock y calcular total
            var detalles = new List<DetalleVenta>();
            decimal total = 0;

            foreach (var item in dto.Detalles)
            {
                var medicamento = await _contexto.Medicamentos
                    .FirstOrDefaultAsync(m => m.Id == item.MedicamentoId && m.Activo);

                if (medicamento == null)
                    throw new Exception($"Medicamento con Id {item.MedicamentoId} no encontrado.");

                if (item.Cantidad <= 0)
                    throw new Exception($"La cantidad del medicamento {medicamento.Nombre} debe ser mayor a cero.");

                if (medicamento.StockActual < item.Cantidad)
                    throw new Exception($"Stock insuficiente para {medicamento.Nombre}. " +
                        $"Stock disponible: {medicamento.StockActual}.");

                var subtotal = medicamento.PrecioVenta * item.Cantidad;
                total += subtotal;

                detalles.Add(new DetalleVenta
                {
                    MedicamentoId = item.MedicamentoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = medicamento.PrecioVenta,
                    Subtotal = subtotal
                });

                // Descontar stock automáticamente
                medicamento.StockActual -= item.Cantidad;
                medicamento.FechaModificacion = DateTime.UtcNow;
            }

            // Generar número de comprobante único
            var numeroComprobante = GenerarNumeroComprobante(dto.TipoComprobante);

            var venta = new Venta
            {
                NumeroComprobante = numeroComprobante,
                TipoComprobante = dto.TipoComprobante,
                Total = total,
                FechaVenta = DateTime.UtcNow,
                PacienteId = dto.PacienteId,
                Detalles = detalles
            };

            _contexto.Ventas.Add(venta);
            await _contexto.SaveChangesAsync();

            // Recargar con navegación completa
            await _contexto.Entry(venta).Reference(v => v.Paciente).LoadAsync();
            foreach (var detalle in venta.Detalles)
            {
                await _contexto.Entry(detalle).Reference(d => d.Medicamento).LoadAsync();
            }

            return MapearDto(venta);
        }

        private string GenerarNumeroComprobante(string tipo)
        {
            var prefijo = tipo == "Boleta" ? "B001" : "F001";
            var numero = new Random().Next(10000000, 99999999);
            return $"{prefijo}-{numero}";
        }

        private DtoVenta MapearDto(Venta v)
        {
            return new DtoVenta
            {
                Id = v.Id,
                NumeroComprobante = v.NumeroComprobante,
                TipoComprobante = v.TipoComprobante,
                Total = v.Total,
                FechaVenta = v.FechaVenta,
                PacienteId = v.PacienteId,
                NombrePaciente = $"{v.Paciente.Nombres} {v.Paciente.Apellidos}",
                Detalles = v.Detalles.Select(d => new DtoDetalleVenta
                {
                    MedicamentoId = d.MedicamentoId,
                    NombreMedicamento = d.Medicamento.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            };
        }
    }
}
