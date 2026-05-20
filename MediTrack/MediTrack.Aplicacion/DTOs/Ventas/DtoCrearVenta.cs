using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Ventas
{
    public class DtoCrearVenta
    {
        public int PacienteId { get; set; }
        public string TipoComprobante { get; set; } = string.Empty; // Boleta, Factura
        public List<DtoCrearDetalle> Detalles { get; set; } = new();
    }
}
