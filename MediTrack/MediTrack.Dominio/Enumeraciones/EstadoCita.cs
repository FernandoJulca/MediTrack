using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Dominio.Enumeraciones
{
    public enum EstadoCita
    {
        Agendada = 1,
        Confirmada = 2,
        Llego = 3,
        EnAtencion = 4,
        Completada = 5,
        NoSePresento = 6,
        Cancelada = 7
    }
}
