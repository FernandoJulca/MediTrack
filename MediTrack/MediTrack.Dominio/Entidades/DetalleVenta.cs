using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Comun;

namespace MediTrack.Dominio.Entidades
{
    public class DetalleVenta : EntidadBase
    {
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        
        //Relaciones
        public int VentaId { get; set; }
        public Venta Venta { get; set; } = null!;

        public int MedicamentoId { get; set; }
        public Medicamento Medicamento { get; set; } = null!;
    }
}
