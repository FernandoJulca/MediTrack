using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Comun;

namespace MediTrack.Dominio.Entidades
{
    public class InformeMedico : EntidadBase
    {
        public string Sintomas { get; set; } = string.Empty;
        public string Diagnostico { get; set; } = string.Empty;
        public string Tratamiento { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public string? Receta { get; set; }
        public DateTime FechaInforme { get; set; } = DateTime.UtcNow;

        // Relación
        public int CitaId { get; set; }
        public Cita Cita { get; set; } = null!;
    }
}
