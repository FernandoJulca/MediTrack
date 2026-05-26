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
                .Include(c => c.Doctor).ThenInclude(d => d.Especialidad)
                .Include(c => c.Sede)
                .Include(c => c.Especialidad)
                .Include(c => c.InformeMedico)
                .Where(c => c.Activo)
                .OrderByDescending(c => c.FechaHora)
                .ToListAsync();

            return citas.Select(c => MapearDto(c)).ToList();
        }

        public async Task<DtoCita> ObtenerPorId(int id)
        {
            var cita = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor).ThenInclude(d => d.Especialidad)
                .Include(c => c.Sede)
                .Include(c => c.Especialidad)
                .Include(c => c.InformeMedico)
                .FirstOrDefaultAsync(c => c.Id == id && c.Activo);

            if (cita == null)
                throw new Exception("Cita no encontrada.");

            return MapearDto(cita);
        }

        public async Task<List<DtoCita>> ObtenerPorPaciente(int pacienteId)
        {
            var citas = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor).ThenInclude(d => d.Especialidad)
                .Include(c => c.Sede)
                .Include(c => c.Especialidad)
                .Include(c => c.InformeMedico)
                .Where(c => c.PacienteId == pacienteId && c.Activo)
                .OrderByDescending(c => c.FechaHora)
                .ToListAsync();

            return citas.Select(c => MapearDto(c)).ToList();
        }

        public async Task<List<DtoCita>> ObtenerPorDoctor(int doctorId)
        {
            var citas = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor).ThenInclude(d => d.Especialidad)
                .Include(c => c.Sede)
                .Include(c => c.Especialidad)
                .Include(c => c.InformeMedico)
                .Where(c => c.DoctorId == doctorId && c.Activo)
                .OrderByDescending(c => c.FechaHora)
                .ToListAsync();

            return citas.Select(c => MapearDto(c)).ToList();
        }

        public async Task<List<DtoCita>> ObtenerPorFechaYSede(DateTime fecha, int sedeId)
        {
            var citas = await _contexto.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor).ThenInclude(d => d.Especialidad)
                .Include(c => c.Sede)
                .Include(c => c.Especialidad)
                .Include(c => c.InformeMedico)
                .Where(c => c.Activo && c.SedeId == sedeId && c.FechaHora.Date == fecha.Date)
                .OrderBy(c => c.FechaHora)
                .ToListAsync();

            return citas.Select(c => MapearDto(c)).ToList();
        }

        public async Task<DtoCita> Crear(DtoCrearCita dto)
        {
            var paciente = await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.Id == dto.PacienteId && u.Activo);
            if (paciente == null)
                throw new Exception("Paciente no encontrado.");

            var doctor = await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.Id == dto.DoctorId && u.Rol == RolUsuario.Doctor && u.Activo);
            if (doctor == null)
                throw new Exception("Doctor no encontrado.");

            var sede = await _contexto.Sedes
                .FirstOrDefaultAsync(s => s.Id == dto.SedeId && s.Activo);
            if (sede == null)
                throw new Exception("Sede no encontrada.");

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
                SedeId = dto.SedeId,
                EspecialidadId = dto.EspecialidadId,
                Estado = EstadoCita.Agendada
            };

            _contexto.Citas.Add(cita);
            await _contexto.SaveChangesAsync();

            return await ObtenerPorId(cita.Id);
        }

        public async Task<DtoCita> CambiarEstado(int id, DtoCambiarEstadoCita dto)
        {
            var cita = await _contexto.Citas
                .FirstOrDefaultAsync(c => c.Id == id && c.Activo);

            if (cita == null)
                throw new Exception("Cita no encontrada.");

            if (cita.Estado == EstadoCita.Cancelada || cita.Estado == EstadoCita.Completada)
                throw new Exception("No se puede modificar una cita cancelada o completada.");

            cita.Estado = (EstadoCita)dto.Estado;
            cita.Observaciones = dto.Observaciones;
            cita.FechaModificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            return await ObtenerPorId(cita.Id);
        }

        public async Task<DtoCita> CrearInformeMedico(DtoCrearInformeMedico dto)
        {
            var cita = await _contexto.Citas
                .FirstOrDefaultAsync(c => c.Id == dto.CitaId && c.Activo);

            if (cita == null)
                throw new Exception("Cita no encontrada.");

            if (cita.Estado != EstadoCita.EnAtencion)
                throw new Exception("Solo se puede crear el informe de una cita en atención.");

            var informe = new InformeMedico
            {
                CitaId = dto.CitaId,
                Sintomas = dto.Sintomas,
                Diagnostico = dto.Diagnostico,
                Tratamiento = dto.Tratamiento,
                Observaciones = dto.Observaciones,
                Receta = dto.Receta,
                FechaInforme = DateTime.UtcNow
            };

            _contexto.InformesMedicos.Add(informe);

            cita.Estado = EstadoCita.Completada;
            cita.FechaModificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            return await ObtenerPorId(cita.Id);
        }

        public async Task Cancelar(int id)
        {
            var cita = await _contexto.Citas
                .FirstOrDefaultAsync(c => c.Id == id && c.Activo);

            if (cita == null)
                throw new Exception("Cita no encontrada.");

            if (cita.Estado == EstadoCita.Completada || cita.Estado == EstadoCita.EnAtencion)
                throw new Exception("No se puede cancelar una cita en atención o completada.");

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
                NombreDoctor = $"{c.Doctor.Nombres} {c.Doctor.Apellidos}",
                SedeId = c.SedeId,
                NombreSede = c.Sede.Nombre,
                EspecialidadId = c.EspecialidadId,
                NombreEspecialidad = c.Especialidad.Nombre,
                InformeMedico = c.InformeMedico == null ? null : new DtoInformeMedico
                {
                    Id = c.InformeMedico.Id,
                    Sintomas = c.InformeMedico.Sintomas,
                    Diagnostico = c.InformeMedico.Diagnostico,
                    Tratamiento = c.InformeMedico.Tratamiento,
                    Observaciones = c.InformeMedico.Observaciones,
                    Receta = c.InformeMedico.Receta,
                    FechaInforme = c.InformeMedico.FechaInforme
                }
            };
        }
    }
}
