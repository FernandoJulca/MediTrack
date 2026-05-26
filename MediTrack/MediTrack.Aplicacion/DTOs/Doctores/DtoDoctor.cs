using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Doctores
{
    public class DtoDoctor
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? Biografia { get; set; }
        public string? UrlFoto { get; set; }
        public int EspecialidadId { get; set; }
        public string NombreEspecialidad { get; set; } = string.Empty;
        public List<DtoHorario> Horarios { get; set; } = new();
    }
}
