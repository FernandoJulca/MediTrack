using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Comun;

namespace MediTrack.Dominio.Entidades
{
    public class Especialidad : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Icono { get; set; }

        // Navegación
        public ICollection<Usuario> Doctores { get; set; } = new List<Usuario>();
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
