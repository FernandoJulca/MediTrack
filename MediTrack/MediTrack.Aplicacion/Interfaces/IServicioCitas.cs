using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Citas;

namespace MediTrack.Aplicacion.Interfaces
{
    public interface IServicioCitas
    {
        Task<List<DtoCita>> ObtenerTodas();
        Task<DtoCita> ObtenerPorId(int id);
        Task<List<DtoCita>> ObtenerPorPaciente(int pacienteId);
        Task<List<DtoCita>> ObtenerPorDoctor(int doctorId);
        Task<List<DtoCita>> ObtenerPorFecha(DateTime fecha);
        Task<DtoCita> Crear(DtoCrearCita dto);
        Task<DtoCita> Actualizar(int id, DtoActualizarCita dto);
        Task<DtoCita> CambiarEstado(int id, DtoCambiarEstadoCita dto);
        Task Cancelar(int id);
    }
}
