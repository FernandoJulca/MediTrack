using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Doctores;

namespace MediTrack.Aplicacion.Interfaces
{
    public interface IServicioDoctores
    {
        Task<List<DtoDoctor>> ObtenerTodos();
        Task<List<DtoDoctor>> ObtenerPorEspecialidad(int especialidadId);
        Task<List<DtoDoctor>> ObtenerPorSede(int sedeId);
        Task<DtoDoctor> ObtenerPorId(int id);
    }
}
