using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Comun;

namespace MediTrack.Dominio.Entidades
{
    public class Medicamento : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Laboratorio { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty; //mg, ml, unidad

        public int StockActual {  get; set; }
        public int StockMinimo { get; set; }
        public int PrecioCompra { get; set; }
        public int PrecioVenta { get; set; }
        public DateTime FechaVencimiento { get; set; }

        //Navegacion

        public ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();
    }
}
