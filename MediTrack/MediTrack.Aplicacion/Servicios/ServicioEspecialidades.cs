using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Especialidades;
using MediTrack.Aplicacion.Interfaces;
using MediTrack.Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Aplicacion.Servicios
{
    public class ServicioEspecialidades :IServicioEspecialidades
    {
        private readonly ContextoAplicacion _contexto;

        public ServicioEspecialidades(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DtoEspecialidad>> ObtenerTodas()
        {
            return await _contexto.Especialidades
                .Where(e => e.Activo)
                .Select(e => new DtoEspecialidad
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Descripcion = e.Descripcion,
                    Icono = e.Icono
                })
                .ToListAsync();
        }

        public async Task<DtoEspecialidad> ObtenerPorId(int id)
        {
            var especialidad = await _contexto.Especialidades
                .FirstOrDefaultAsync(e => e.Id == id && e.Activo);

            if (especialidad == null)
                throw new Exception("Especialidad no encontrada.");

            return new DtoEspecialidad
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre,
                Descripcion = especialidad.Descripcion,
                Icono = especialidad.Icono
            };
        }
    }
}
