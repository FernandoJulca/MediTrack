using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Dominio.Comun
{
    public abstract class EntidadBase
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }
        public bool Activo { get; set; } = true;

    }
}
