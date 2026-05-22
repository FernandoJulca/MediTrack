using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Infraestructura.Datos
{
    public class ContextoAplicacion : DbContext
    {
        public ContextoAplicacion(DbContextOptions<ContextoAplicacion> opciones)
            :base(opciones) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Sede> Sedes { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<HorarioDoctor> HorariosDoctores { get; set; }
        public DbSet<InformeMedico> InformesMedicos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar todas las configuraciones de la carpeta Configuraciones
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoAplicacion).Assembly);

        }
    }
}
