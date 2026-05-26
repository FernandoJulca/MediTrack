using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Sedes;
using MediTrack.Aplicacion.Interfaces;
using MediTrack.Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Aplicacion.Servicios
{
    public class ServicioSedes : IServicioSedes
    {
        private readonly ContextoAplicacion _contexto;

        public ServicioSedes(ContextoAplicacion contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DtoSede>> ObtenerTodas()
        {
            return await _contexto.Sedes
                .Where(s => s.Activo)
                .Select(s => new DtoSede
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Direccion = s.Direccion,
                    Telefono = s.Telefono,
                    Ciudad = s.Ciudad,
                    Descripcion = s.Descripcion,
                    UrlFoto = s.UrlFoto
                })
                .ToListAsync();
        }

        public async Task<DtoSede> ObtenerPorId(int id)
        {
            var sede = await _contexto.Sedes
                .FirstOrDefaultAsync(s => s.Id == id && s.Activo);

            if (sede == null)
                throw new Exception("Sede no encontrada.");

            return new DtoSede
            {
                Id = sede.Id,
                Nombre = sede.Nombre,
                Direccion = sede.Direccion,
                Telefono = sede.Telefono,
                Ciudad = sede.Ciudad,
                Descripcion = sede.Descripcion,
                UrlFoto = sede.UrlFoto
            };
        }
    }
}
