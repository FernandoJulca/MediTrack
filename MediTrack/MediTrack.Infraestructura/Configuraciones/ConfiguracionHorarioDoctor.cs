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
    public class ConfiguracionHorarioDoctor :IEntityTypeConfiguration<HorarioDoctor>
    {
        public void Configure(EntityTypeBuilder<HorarioDoctor> constructor)
        {
            constructor.ToTable("HorariosDoctores");
            constructor.HasKey(h => h.Id);

            constructor.HasOne(h => h.Doctor)
                .WithMany(u => u.Horarios)
                .HasForeignKey(h => h.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            constructor.HasOne(h => h.Sede)
                .WithMany(s => s.Horarios)
                .HasForeignKey(h => h.SedeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
