using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Comun;

namespace MediTrack.Dominio.Entidades
{
    public class Venta : EntidadBase
    {
        public string NumeroComprobante { get; set; } = string.Empty;
        public string TipoComprobante { get; set; } = string.Empty; //boleta, factura
        public decimal Total {  get; set; }
        public DateTime FechaVenta { get; set; } = DateTime.UtcNow;

        //Relaciones

        public int PacienteId {  get; set; }
        public Usuario Paciente { get; set; } = null!;

        public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
