using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Comun;
using MediTrack.Dominio.Enumeraciones;

namespace MediTrack.Dominio.Entidades
{
    public class Cita : EntidadBase
    {
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public EstadoCita Estado {  get; set; } = EstadoCita.Pediente;

        //Relaciones
        public int PacienteId { get; set; }
        public Usuario Paciente { get; set; } = null!;

        public int DoctorId { get; set; }
        public Usuario Doctor { get; set; } = null!;

    }
}
