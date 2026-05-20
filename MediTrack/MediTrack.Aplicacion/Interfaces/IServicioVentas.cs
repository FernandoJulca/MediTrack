using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Ventas;

namespace MediTrack.Aplicacion.Interfaces
{
    public interface IServicioVentas
    {
        Task<List<DtoVenta>> ObtenerTodas();
        Task<DtoVenta> ObtenerPorId(int id);
        Task<List<DtoVenta>> ObtenerPorPaciente(int pacienteId);
        Task<DtoVenta> Crear(DtoCrearVenta dto);
    }
}
