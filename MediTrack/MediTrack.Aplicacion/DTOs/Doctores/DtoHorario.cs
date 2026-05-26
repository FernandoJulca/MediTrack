using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Doctores
{
    public class DtoHorario
    {
        public int DiaSemana { get; set; }
        public string NombreDia { get; set; } = string.Empty;
        public string HoraInicio { get; set; } = string.Empty;
        public string HoraFin { get; set; } = string.Empty;
        public int DuracionCitaMinutos { get; set; }
        public int SedeId { get; set; }
        public string NombreSede { get; set; } = string.Empty;
    }
}
