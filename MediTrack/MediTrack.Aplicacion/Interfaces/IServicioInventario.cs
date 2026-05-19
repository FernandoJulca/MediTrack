using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Inventario;

namespace MediTrack.Aplicacion.Interfaces
{
    public interface IServicioInventario
    {
        Task<List<DtoMedicamentos>> ObtenerTodos();
        Task<DtoMedicamentos> ObtenerPorId(int id);
        Task<DtoMedicamentos> Crear(DtoCrearMedicamento dto);
        Task<DtoMedicamentos> Actualizar(int id, DtoActualizarMedicamento dto);
        Task Eliminar(int id);
        Task<List<DtoMedicamentos>> ObtenerConStockBajo();
        Task<List<DtoMedicamentos>> ObtenerPorVencer(int dias);
        Task<DtoMedicamentos> AjustarStock(DtoAjustarStock dto);
    }
}
