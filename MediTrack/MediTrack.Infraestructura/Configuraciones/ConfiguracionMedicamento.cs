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
    public class ConfiguracionMedicamento : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> constructor)
        {
            constructor.ToTable("Medicamentos");
            constructor.HasKey(m => m.Id);

            constructor.Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(150);
            constructor.Property(m => m.Descripcion)
                .HasMaxLength(500);
            constructor.Property(m => m.Laboratorio)
                .HasMaxLength(100);
            constructor.Property(m => m.UnidadMedida)
                .HasMaxLength(20);
            constructor.Property(m => m.PrecioCompra)
                .HasColumnType("decimal(10,2)");
            constructor.Property(m => m.PrecioVenta)
                .HasColumnType("decimal(10,2)");
        }
    }
}
