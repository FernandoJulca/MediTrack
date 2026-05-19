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
    public class ConfiguracionDetalleVenta : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> constructor)
        {
            constructor.ToTable("DetallesVenta");

            constructor.HasKey(d => d.Id);

            constructor.Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(10,2)");

            constructor.Property(d => d.Subtotal)
                .HasColumnType("decimal(10,2)");

            constructor.HasOne(d => d.Venta)
                .WithMany(v => v.Detalles)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.Cascade);

            constructor.HasOne(d => d.Medicamento)
                .WithMany(m => m.DetallesVenta)
                .HasForeignKey(d => d.MedicamentoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
