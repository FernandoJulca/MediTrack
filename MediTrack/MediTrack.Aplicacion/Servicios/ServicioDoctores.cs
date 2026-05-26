using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Doctores;
using MediTrack.Aplicacion.Interfaces;
using MediTrack.Dominio.Enumeraciones;
using MediTrack.Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Aplicacion.Servicios
{
    public class ServicioDoctores : IServicioDoctores
    {
        private readonly ContextoAplicacion _contexto;
        private readonly string[] _diasSemana = { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

        public ServicioDoctores(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DtoDoctor>> ObtenerTodos()
        {
            var doctores = await _contexto.Usuarios
                .Include(u => u.Especialidad)
                .Include(u => u.Horarios).ThenInclude(h => h.Sede)
                .Where(u => u.Rol == RolUsuario.Doctor && u.Activo)
                .ToListAsync();

            return doctores.Select(d => MapearDto(d)).ToList();
        }

        public async Task<List<DtoDoctor>> ObtenerPorEspecialidad(int especialidadId)
        {
            var doctores = await _contexto.Usuarios
                .Include(u => u.Especialidad)
                .Include(u => u.Horarios).ThenInclude(h => h.Sede)
                .Where(u => u.Rol == RolUsuario.Doctor && u.EspecialidadId == especialidadId && u.Activo)
                .ToListAsync();

            return doctores.Select(d => MapearDto(d)).ToList();
        }

        public async Task<List<DtoDoctor>> ObtenerPorSede(int sedeId)
        {
            var doctores = await _contexto.Usuarios
                .Include(u => u.Especialidad)
                .Include(u => u.Horarios).ThenInclude(h => h.Sede)
                .Where(u => u.Rol == RolUsuario.Doctor && u.Activo
                    && u.Horarios.Any(h => h.SedeId == sedeId && h.Activo))
                .ToListAsync();

            return doctores.Select(d => MapearDto(d)).ToList();
        }

        public async Task<DtoDoctor> ObtenerPorId(int id)
        {
            var doctor = await _contexto.Usuarios
                .Include(u => u.Especialidad)
                .Include(u => u.Horarios).ThenInclude(h => h.Sede)
                .FirstOrDefaultAsync(u => u.Id == id && u.Rol == RolUsuario.Doctor && u.Activo);

            if (doctor == null)
                throw new Exception("Doctor no encontrado.");

            return MapearDto(doctor);
        }

        private DtoDoctor MapearDto(MediTrack.Dominio.Entidades.Usuario d)
        {
            return new DtoDoctor
            {
                Id = d.Id,
                NombreCompleto = $"{d.Nombres} {d.Apellidos}",
                Correo = d.Correo,
                Telefono = d.Telefono,
                Biografia = d.Biografia,
                UrlFoto = d.UrlFoto,
                EspecialidadId = d.EspecialidadId ?? 0,
                NombreEspecialidad = d.Especialidad?.Nombre ?? "",
                Horarios = d.Horarios.Where(h => h.Activo).Select(h => new DtoHorario
                {
                    DiaSemana = h.DiaSemana,
                    NombreDia = _diasSemana[h.DiaSemana],
                    HoraInicio = h.HoraInicio.ToString("HH:mm"),
                    HoraFin = h.HoraFin.ToString("HH:mm"),
                    DuracionCitaMinutos = h.DuracionCitaMinutos,
                    SedeId = h.SedeId,
                    NombreSede = h.Sede.Nombre
                }).ToList()
            };
        }
    }
}
