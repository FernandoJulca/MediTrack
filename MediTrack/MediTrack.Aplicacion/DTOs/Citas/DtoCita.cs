using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Citas
{
    public class DtoCita
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; } = string.Empty;
        public int DoctorId { get; set; }
        public string NombreDoctor { get; set; } = string.Empty;
        public int SedeId { get; set; }
        public string NombreSede { get; set; } = string.Empty;
        public int EspecialidadId { get; set; }
        public string NombreEspecialidad { get; set; } = string.Empty;
        public DtoInformeMedico? InformeMedico { get; set; }
    }
}
