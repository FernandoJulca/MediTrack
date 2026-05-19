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
        public RolUsuario Rol {  get; set; }

        //Navegacion
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }

}