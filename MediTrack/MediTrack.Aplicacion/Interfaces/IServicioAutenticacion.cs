using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Aplicacion.DTOs.Autenticacion;

namespace MediTrack.Aplicacion.Interfaces
{
    public interface IServicioAutenticacion
    {
        Task<DtoRespuestaAuth> Registrar(DtoRegistro dto);
        Task<DtoRespuestaAuth> Login(DtoLogin dto);
    }
}
