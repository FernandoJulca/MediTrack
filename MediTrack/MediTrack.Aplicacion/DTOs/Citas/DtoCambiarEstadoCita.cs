using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Citas
{
    public class DtoCambiarEstadoCita
    {
        public int Estado { get; set; } // 1 Pendiente, 2 Confirmada, 3 Cancelada, 4 Completada
        public string? Observaciones { get; set; }
    }
}
