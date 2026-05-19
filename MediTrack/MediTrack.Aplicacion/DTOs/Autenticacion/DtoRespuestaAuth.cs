using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediTrack.Aplicacion.DTOs.Autenticacion
{
    public class DtoRespuestaAuth
    {
        public string Token { get; set; } = string.Empty;
        public string Correo {  get; set; } = string.Empty;
        public string NombreCompleto {  get; set; } = string.Empty;
        public string Rol {  get; set; } = string.Empty;
        public DateTime Expiracion {  get; set; }
    }
}
