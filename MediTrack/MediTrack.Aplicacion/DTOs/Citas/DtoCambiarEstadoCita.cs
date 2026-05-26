using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Citas
{
    public class DtoCambiarEstadoCita
    {
        public int Estado { get; set; }
        public string? Observaciones { get; set; }
    }
}
