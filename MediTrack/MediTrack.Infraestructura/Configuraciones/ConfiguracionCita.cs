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
            constructor.Property(c => c.Motivo)
                .IsRequired()
                .HasMaxLength(250);
            constructor.Property(c => c.Observaciones)
                .HasMaxLength(500);
            constructor.Property(c => c.Estado)
                .IsRequired();

            //Relacion Cita -> Paciente
            constructor.HasOne(c => c.Paciente)
                .WithMany(u => u.Citas)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
            //Relacion Cita -> Doctor (sin navegacion inversa para evitar ciclo)
            constructor.HasOne(c => c.Doctor)
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
