using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MediTrack.Aplicacion.DTOs.Inventario;
using MediTrack.Aplicacion.Interfaces;
using MediTrack.Dominio.Entidades;
using MediTrack.Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Aplicacion.Servicios
{
    public class ServicioInventario : IServicioInventario
    {
        private readonly ContextoAplicacion _contexto;

        public ServicioInventario(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DtoMedicamentos>> ObtenerTodos()
        {
            var medicamentos = await _contexto.Medicamentos
                .Where(m => m.Activo)
                .OrderBy(m => m.Nombre)
                .ToListAsync();
            
            return medicamentos.Select(m => MapearDto(m)).ToList();
        }

        public async Task<DtoMedicamentos> ObtenerPorId(int id)
        {
            var medicamento = await _contexto.Medicamentos
                .FirstOrDefaultAsync(m => m.Id == id && m.Activo);

            if (medicamento == null)
            {
                throw new Exception("Medicamento no encontrado.");
            }

            return MapearDto(medicamento);
        }

        public async Task<DtoMedicamentos> Crear(DtoCrearMedicamento dto)
        {
            var existe = await _contexto.Medicamentos
                .AnyAsync(m => m.Nombre == dto.Nombre && m.Activo);

            if (existe)
            {
                throw new Exception("Ya existe un medicamento con ese nombre.");
            }

            var medicamento = new Medicamento
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Laboratorio = dto.Laboratorio,
                UnidadMedida = dto.UnidadMedida,
                StockActual = dto.StockActual,
                StockMinimo = dto.StockMinimo,
                PrecioCompra = dto.PrecioCompra,
                PrecioVenta = dto.PrecioVenta,
                FechaVencimiento = dto.FechaVencimiento
            };

            _contexto.Medicamentos .Add(medicamento);
            await _contexto.SaveChangesAsync();

            return MapearDto(medicamento);

        }

        public async Task<DtoMedicamentos> Actualizar(int id, DtoActualizarMedicamento dto)
        {
            var medicamento = await _contexto.Medicamentos
                .FirstOrDefaultAsync(m => m.Id == id && m.Activo);

            if (medicamento == null)
            {
                throw new Exception("Medicamento no encontrado.");
            }

            medicamento.Nombre = dto.Nombre;
            medicamento.Descripcion = dto.Descripcion;
            medicamento.Laboratorio = dto.Laboratorio;
            medicamento.UnidadMedida = dto.UnidadMedida;
            medicamento.StockMinimo = dto.StockMinimo;
            medicamento.PrecioCompra = dto.PrecioCompra;
            medicamento.PrecioVenta = dto.PrecioVenta;
            medicamento.FechaVencimiento = dto.FechaVencimiento;
            medicamento.FechaModificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            return MapearDto(medicamento);

        }
        
        public async Task Eliminar(int id)
        {
            var medicamento = await _contexto.Medicamentos
                .FirstOrDefaultAsync(m => m.Id == id && m.Activo);

            if (medicamento == null)
            {
                throw new Exception("Medicamento no encontrado.");
            }

            //Eliminacion lógica
            medicamento.Activo = false;
            medicamento.FechaModificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();
        }

        public async Task<List<DtoMedicamentos>> ObtenerConStockBajo()
        {
            var medicamento = await _contexto.Medicamentos
                .Where(m => m.Activo && m.StockActual <= m.StockMinimo)
                .OrderBy(m => m.StockActual)
                .ToListAsync();

            return medicamento.Select(m => MapearDto(m)).ToList();
        }

        public async Task<List<DtoMedicamentos>> ObtenerPorVencer(int dias)
        {
            var fechalimite = DateTime.UtcNow.AddDays(dias);

            var medicamento = await _contexto.Medicamentos
                .Where(m => m.Activo && m.FechaVencimiento <= fechalimite)
                .OrderBy(m => m.FechaVencimiento)
                .ToListAsync();

            return medicamento.Select(m => MapearDto(m)).ToList();
        }

        public async Task<DtoMedicamentos> AjustarStock(DtoAjustarStock dto)
        {
            var medicamento = await _contexto.Medicamentos
                .FirstOrDefaultAsync(m => m.Id == dto.MedicamentoId && m.Activo);

            if (medicamento == null)
            {
                throw new Exception("Medicamento no encontrado.");
            }

            var nuevoStock = medicamento.StockActual + dto.Cantidad;

            if (nuevoStock < 0)
            {
                throw new Exception("El stock no puede quedar en negativo.");
            }

            medicamento.StockActual = nuevoStock;
            medicamento.FechaModificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            return MapearDto(medicamento);
        }



        // Método privado para mapear entidad → DTO
        private DtoMedicamentos MapearDto(Medicamento m)
        {
            return new DtoMedicamentos
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Descripcion = m.Descripcion,
                Laboratorio = m.Laboratorio,
                UnidadMedida = m.UnidadMedida,
                StockActual = m.StockActual,
                StockMinimo = m.StockMinimo,
                PrecioCompra = m.PrecioCompra,
                PrecioVenta = m.PrecioVenta,
                FechaVencimiento = m.FechaVencimiento,
                StockBajo = m.StockActual <= m.StockMinimo,
                PorVencer = m.FechaVencimiento <= DateTime.UtcNow.AddDays(30)
            };
        }
    }
}
