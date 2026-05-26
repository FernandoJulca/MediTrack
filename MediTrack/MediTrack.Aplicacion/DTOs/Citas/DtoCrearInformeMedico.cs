using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Citas
{
    public class DtoCrearInformeMedico
    {
        public int CitaId { get; set; }
        public string Sintomas { get; set; } = string.Empty;
        public string Diagnostico { get; set; } = string.Empty;
        public string Tratamiento { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public string? Receta { get; set; }
    }
}
