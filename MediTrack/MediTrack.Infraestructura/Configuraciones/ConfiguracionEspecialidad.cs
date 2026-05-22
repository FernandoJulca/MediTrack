using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediTrack.Infraestructura.Configuraciones
{
    public class ConfiguracionEspecialidad : IEntityTypeConfiguration<Especialidad>
    {
        public void Configure(EntityTypeBuilder<Especialidad> constructor)
        {
            constructor.ToTable("Especialidades");
            constructor.HasKey(e => e.Id);
            constructor.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            constructor.Property(e => e.Descripcion).HasMaxLength(500);
            constructor.Property(e => e.Icono).HasMaxLength(50);
        }
    }
}
