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
    public class ConfiguracionSede : IEntityTypeConfiguration<Sede>
    {
        public void Configure(EntityTypeBuilder<Sede> constructor)
        {
            constructor.ToTable("Sedes");
            constructor.HasKey(s => s.Id);
            constructor.Property(s => s.Nombre).IsRequired().HasMaxLength(150);
            constructor.Property(s => s.Direccion).IsRequired().HasMaxLength(250);
            constructor.Property(s => s.Telefono).HasMaxLength(15);
            constructor.Property(s => s.Ciudad).HasMaxLength(100);
            constructor.Property(s => s.Descripcion).HasMaxLength(500);
        }
    }
}
