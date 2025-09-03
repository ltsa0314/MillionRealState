using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionRealState.Domain.Aggregates.Property;

namespace MillionRealState.Infrastructure.Configurations
{
    /// <summary>
    /// Configuración de EF Core para el agregado <see cref="PropertyAggregate"/>.
    /// Define el mapeo de la entidad PropertyAggregate a la tabla 'Properties' en la base de datos,
    /// incluyendo sus propiedades, restricciones, el value object Address y la relación con imágenes.
    /// </summary>
    internal class PropertyConfiguration : IEntityTypeConfiguration<PropertyAggregate>
    {
        /// <summary>
        /// Configura el esquema de la entidad <see cref="PropertyAggregate"/> para EF Core.
        /// </summary>
        /// <param name="builder">Constructor de la entidad para definir el mapeo.</param>
        public void Configure(EntityTypeBuilder<PropertyAggregate> builder)
        {
            // Nombre de la tabla en plural
            builder.ToTable("Properties");

            // Clave primaria
            builder.HasKey(p => p.IdProperty);

            // Propiedad Name: requerida, longitud máxima 100
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Propiedad Price: requerida
            builder.Property(p => p.Price)
                .IsRequired();

            // Propiedad CodeInternal: requerida, longitud máxima 50
            builder.Property(p => p.CodeInternal)
                .IsRequired()
                .HasMaxLength(50);

            // Propiedad Year: requerida
            builder.Property(p => p.Year)
                .IsRequired();

            // Propiedad IdOwner: requerida
            builder.Property(p => p.IdOwner)
                .IsRequired();

            // Value Object Address: configuración de propiedades internas
            builder.OwnsOne(p => p.Address, address =>
            {
                address.Property(a => a.Street).IsRequired().HasMaxLength(100);
                address.Property(a => a.City).IsRequired().HasMaxLength(50);
                address.Property(a => a.State).HasMaxLength(50);
                address.Property(a => a.ZipCode).HasMaxLength(20);
                address.Property(a => a.Country).HasMaxLength(50);
            });

            // Propiedades comunes de AggregateRoot
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.CreatedBy).HasMaxLength(100);

            // Relación con imágenes (uno a muchos)
            builder.HasMany(p => p.Images)
                .WithOne()
                .HasForeignKey(img => img.IdProperty)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}