using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Especialidades;

namespace MediTrack.Aplicacion.Interfaces
{
    public interface IServicioEspecialidades
    {
        Task<List<DtoEspecialidad>> ObtenerTodas();
        Task<DtoEspecialidad> ObtenerPorId(int id);
    }
}
