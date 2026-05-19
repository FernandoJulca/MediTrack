using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Inventario
{
    public class DtoAjustarStock
    {
        public int MedicamentoId { get; set; }
        public int Cantidad { get; set; }
        public string Motivo { get; set; }  = string.Empty;
    }
}
