using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionRealState.Domain.Aggregates.Owner;

namespace MillionRealState.Infrastructure.Configurations
{
    /// <summary>
    /// Configuración de EF Core para el agregado <see cref="OwnerAggregate"/>.
    /// Define el mapeo de la entidad OwnerAggregate a la tabla 'Owners' en la base de datos,
    /// incluyendo sus propiedades, restricciones y el value object Address.
    /// </summary>
    internal class OwnerConfiguration : IEntityTypeConfiguration<OwnerAggregate>
    {
        /// <summary>
        /// Configura el esquema de la entidad <see cref="OwnerAggregate"/> para EF Core.
        /// </summary>
        /// <param name="builder">Constructor de la entidad para definir el mapeo.</param>
        public void Configure(EntityTypeBuilder<OwnerAggregate> builder)
        {
            // Nombre de la tabla en plural
            builder.ToTable("Owners");

            // Clave primaria
            builder.HasKey(o => o.Id);

            // Propiedad Name: requerida, longitud máxima 100
            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Propiedad Photo: longitud máxima 250
            builder.Property(o => o.Photo)
                .HasMaxLength(250);

            // Propiedad Birthday: requerida
            builder.Property(o => o.Birthday)
                .IsRequired();

            // Value Object Address: configuración de propiedades internas
            builder.OwnsOne(o => o.Address, address =>
            {
                address.Property(a => a.Street).IsRequired().HasMaxLength(100);
                address.Property(a => a.City).IsRequired().HasMaxLength(50);
                address.Property(a => a.State).HasMaxLength(50);
                address.Property(a => a.ZipCode).HasMaxLength(20);
                address.Property(a => a.Country).HasMaxLength(50);
            });

            // Propiedades comunes de AggregateRoot
            builder.Property(o => o.CreatedAt).IsRequired();
            builder.Property(o => o.CreatedBy).HasMaxLength(100);
        }
    }
}
