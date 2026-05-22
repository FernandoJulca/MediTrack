using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Comun;

namespace MediTrack.Dominio.Entidades
{
    public class HorarioDoctor : EntidadBase
    {
        public int DiaSemana { get; set; } // 0 Domingo, 1 Lunes ... 6 Sábado
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public int DuracionCitaMinutos { get; set; } = 30;

        // Relaciones
        public int DoctorId { get; set; }
        public Usuario Doctor { get; set; } = null!;

        public int SedeId { get; set; }
        public Sede Sede { get; set; } = null!;
    }
}
