using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Comun;
using MediTrack.Dominio.Enumeraciones;

namespace MediTrack.Dominio.Entidades
{
    public class Usuario : EntidadBase
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string ContrasenaHash { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string? UrlFoto { get; set; }
        public string? Biografia { get; set; }  // Para doctores
        public RolUsuario Rol { get; set; }

        // Solo para doctores
        public int? EspecialidadId { get; set; }
        public Especialidad? Especialidad { get; set; }

        // Navegación
        public ICollection<Cita> CitasPaciente { get; set; } = new List<Cita>();
        public ICollection<Cita> CitasDoctor { get; set; } = new List<Cita>();
        public ICollection<HorarioDoctor> Horarios { get; set; } = new List<HorarioDoctor>();

    }

}