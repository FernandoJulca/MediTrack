using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Citas;
using MediTrack.Aplicacion.Interfaces;
using MediTrack.Dominio.Entidades;
using MediTrack.Dominio.Enumeraciones;
using MediTrack.Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Aplicacion.Servicios
{
    public class ServicioCitas : IServicioCitas
    {
        private readonly ContextoAplicacion _contexto;

        public ServicioCitas(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DtoCita>> ObtenerTodas()
        {
            var citas = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Where(c => c.Activo)
                .OrderByDescending(c => c.FechaHora)
                .ToListAsync();

            return citas.Select(c => MapearDto(c)).ToList();
        }

        public async Task<DtoCita> ObtenerPorId(int id)
        {
            var cita = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .FirstOrDefaultAsync(c => c.Id == id && c.Activo);

            if (cita == null)
                throw new Exception("Cita no encontrada.");

            return MapearDto(cita);
        }

        public async Task<List<DtoCita>> ObtenerPorPaciente(int pacienteId)
        {
            var existePaciente = await _contexto.Usuarios
                .AnyAsync(u => u.Id == pacienteId && u.Activo);

            if (!existePaciente)
                throw new Exception("Paciente no encontrado.");

            var citas = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Where(c => c.PacienteId == pacienteId && c.Activo)
                .OrderByDescending(c => c.FechaHora)
                .ToListAsync();

            return citas.Select(c => MapearDto(c)).ToList();
        }

        public async Task<List<DtoCita>> ObtenerPorDoctor(int doctorId)
        {
            var existeDoctor = await _contexto.Usuarios
                .AnyAsync(u => u.Id == doctorId && u.Activo);

            if (!existeDoctor)
                throw new Exception("Doctor no encontrado.");

            var citas = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Where(c => c.DoctorId == doctorId && c.Activo)
                .OrderByDescending(c => c.FechaHora)
                .ToListAsync();

            return citas.Select(c => MapearDto(c)).ToList();
        }

        public async Task<List<DtoCita>> ObtenerPorFecha(DateTime fecha)
        {
            var citas = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Where(c => c.Activo &&
                            c.FechaHora.Date == fecha.Date)
                .OrderBy(c => c.FechaHora)
                .ToListAsync();

            return citas.Select(c => MapearDto(c)).ToList();
        }

        public async Task<DtoCita> Crear(DtoCrearCita dto)
        {
            // Verificar que el paciente existe
            var paciente = await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.Id == dto.PacienteId && u.Activo);

            if (paciente == null)
                throw new Exception("Paciente no encontrado.");

            // Verificar que el doctor existe
            var doctor = await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.Id == dto.DoctorId
                    && u.Rol == RolUsuario.Doctor && u.Activo);

            if (doctor == null)
                throw new Exception("Doctor no encontrado.");

            // Verificar que el doctor no tenga otra cita en ese horario
            var existeConflicto = await _contexto.Citas
                .AnyAsync(c => c.DoctorId == dto.DoctorId
                    && c.FechaHora == dto.FechaHora
                    && c.Estado != EstadoCita.Cancelada
                    && c.Activo);

            if (existeConflicto)
                throw new Exception("El doctor ya tiene una cita en ese horario.");

            var cita = new Cita
            {
                FechaHora = dto.FechaHora,
                Motivo = dto.Motivo,
                PacienteId = dto.PacienteId,
                DoctorId = dto.DoctorId,
                Estado = EstadoCita.Pendiente
            };

            _contexto.Citas.Add(cita);
            await _contexto.SaveChangesAsync();

            // Recargar con los datos de navegación
            await _contexto.Entry(cita).Reference(c => c.Paciente).LoadAsync();
            await _contexto.Entry(cita).Reference(c => c.Doctor).LoadAsync();

            return MapearDto(cita);
        }

        public async Task<DtoCita> Actualizar(int id, DtoActualizarCita dto)
        {
            var cita = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .FirstOrDefaultAsync(c => c.Id == id && c.Activo);

            if (cita == null)
                throw new Exception("Cita no encontrada.");

            if (cita.Estado == EstadoCita.Cancelada || cita.Estado == EstadoCita.Completada)
                throw new Exception("No se puede modificar una cita cancelada o completada.");

            cita.FechaHora = dto.FechaHora;
            cita.Motivo = dto.Motivo;
            cita.Observaciones = dto.Observaciones;
            cita.FechaModificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            return MapearDto(cita);
        }

        public async Task<DtoCita> CambiarEstado(int id, DtoCambiarEstadoCita dto)
        {
            var cita = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .FirstOrDefaultAsync(c => c.Id == id && c.Activo);

            if (cita == null)
                throw new Exception("Cita no encontrada.");

            if (cita.Estado == EstadoCita.Cancelada || cita.Estado == EstadoCita.Completada)
                throw new Exception("No se puede cambiar el estado de una cita cancelada o completada.");

            cita.Estado = (EstadoCita)dto.Estado;
            cita.Observaciones = dto.Observaciones;
            cita.FechaModificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            return MapearDto(cita);
        }

        public async Task Cancelar(int id)
        {
            var cita = await _contexto.Citas
                .FirstOrDefaultAsync(c => c.Id == id && c.Activo);

            if (cita == null)
                throw new Exception("Cita no encontrada.");

            if (cita.Estado == EstadoCita.Completada)
                throw new Exception("No se puede cancelar una cita ya completada.");

            cita.Estado = EstadoCita.Cancelada;
            cita.FechaModificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();
        }

        private DtoCita MapearDto(Cita c)
        {
            return new DtoCita
            {
                Id = c.Id,
                FechaHora = c.FechaHora,
                Motivo = c.Motivo,
                Observaciones = c.Observaciones,
                Estado = c.Estado.ToString(),
                PacienteId = c.PacienteId,
                NombrePaciente = $"{c.Paciente.Nombres} {c.Paciente.Apellidos}",
                DoctorId = c.DoctorId,
                NombreDoctor = $"{c.Doctor.Nombres} {c.Doctor.Apellidos}"
            };
        }
    }
}
