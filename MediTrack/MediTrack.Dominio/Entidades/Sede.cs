using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Comun;

namespace MediTrack.Dominio.Entidades
{
    public class Sede : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? UrlFoto { get; set; }

        // Navegación
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
        public ICollection<HorarioDoctor> Horarios { get; set; } = new List<HorarioDoctor>();

    }
}
