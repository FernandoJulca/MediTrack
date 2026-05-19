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
    public class ConfiguracionVenta : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> constructor)
        {
            constructor.ToTable("Ventas");

            constructor.HasKey(v => v.Id);

            constructor.Property(v => v.NumeroComprobante)
                .IsRequired()
                .HasMaxLength(20);

            constructor.HasIndex(v => v.NumeroComprobante)
                .IsUnique();

            constructor.Property(v => v.TipoComprobante)
                .IsRequired()
                .HasMaxLength(10);

            constructor.Property(v => v.Total)
                .HasColumnType("decimal(10,2)");

            constructor.HasOne(v => v.Paciente)
                .WithMany()
                .HasForeignKey(v => v.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
