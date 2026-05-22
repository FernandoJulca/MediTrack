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
    public class ConfiguracionCita : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> constructor)
        {
            constructor.ToTable("Citas");
            constructor.HasKey(c => c.Id);

            constructor.Property(c => c.Motivo).IsRequired().HasMaxLength(250);
            constructor.Property(c => c.Observaciones).HasMaxLength(500);
            constructor.Property(c => c.Estado).IsRequired();

            constructor.HasOne(c => c.Paciente)
                .WithMany(u => u.CitasPaciente)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            constructor.HasOne(c => c.Doctor)
                .WithMany(u => u.CitasDoctor)
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            constructor.HasOne(c => c.Sede)
                .WithMany(s => s.Citas)
                .HasForeignKey(c => c.SedeId)
                .OnDelete(DeleteBehavior.Restrict);

            constructor.HasOne(c => c.Especialidad)
                .WithMany(e => e.Citas)
                .HasForeignKey(c => c.EspecialidadId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
