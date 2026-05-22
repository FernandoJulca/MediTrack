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
    public class ConfiguracionInformeMedico : IEntityTypeConfiguration<InformeMedico>
    {
        public void Configure(EntityTypeBuilder<InformeMedico> constructor)
        {
            constructor.ToTable("InformesMedicos");
            constructor.HasKey(i => i.Id);

            constructor.Property(i => i.Sintomas).IsRequired().HasMaxLength(1000);
            constructor.Property(i => i.Diagnostico).IsRequired().HasMaxLength(1000);
            constructor.Property(i => i.Tratamiento).IsRequired().HasMaxLength(1000);
            constructor.Property(i => i.Observaciones).HasMaxLength(1000);
            constructor.Property(i => i.Receta).HasMaxLength(2000);

            constructor.HasOne(i => i.Cita)
                .WithOne(c => c.InformeMedico)
                .HasForeignKey<InformeMedico>(i => i.CitaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
