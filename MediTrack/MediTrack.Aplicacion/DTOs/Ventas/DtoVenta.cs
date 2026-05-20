using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Ventas
{
    public class DtoVenta
    {
        public int Id { get; set; }
        public string NumeroComprobante { get; set; } = string.Empty;
        public string TipoComprobante { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public DateTime FechaVenta { get; set; }
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; } = string.Empty;
        public List<DtoDetalleVenta> Detalles { get; set; } = new();
    }
}
