using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Inventario
{
    public class DtoCrearMedicamento
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion {  get; set; } = string.Empty;
        public string Laboratorio { get; set; } = string.Empty;
        public string UnidadMedida {  get; set; } = string.Empty;
        public int StockActual {  get; set; }
        public int StockMinimo { get; set; }
        public decimal PrecioCompra {  get; set; }
        public decimal PrecioVenta { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
