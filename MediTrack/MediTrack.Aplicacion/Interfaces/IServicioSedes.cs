using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Sedes;

namespace MediTrack.Aplicacion.Interfaces
{
    public interface IServicioSedes
    {
        Task<List<DtoSede>> ObtenerTodas();
        Task<DtoSede> ObtenerPorId(int id);
    }
}
