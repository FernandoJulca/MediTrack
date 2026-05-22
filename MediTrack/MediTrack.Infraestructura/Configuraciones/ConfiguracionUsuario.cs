using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Infraestructura.Configuraciones
{
    public class ConfiguracionUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> constructor)
        {
            constructor.ToTable("Usuarios");
            constructor.HasKey(u => u.Id);
            constructor.Property(u => u.Nombres)
                .IsRequired()
                .HasMaxLength(100);
            constructor.Property(u => u.Apellidos)
                .IsRequired()
                .HasMaxLength(100);
            constructor.Property(u => u.Correo)
                .IsRequired()
                .HasMaxLength(150);
            constructor.HasIndex(u => u.Correo)
                .IsUnique();
            constructor.Property(u => u.ContrasenaHash)
                .IsRequired();
            constructor.Property(u => u.Telefono)
                .HasMaxLength(15);
            constructor.Property(u => u.Dni)
                .IsRequired()
                .HasMaxLength(8);
            constructor.HasIndex(u => u.Dni)
                .IsUnique();
            constructor.Property(u => u.Rol)
                .IsRequired();
            constructor.HasOne(u => u.Especialidad)
                .WithMany(e => e.Doctores)
                .HasForeignKey(u => u.EspecialidadId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
