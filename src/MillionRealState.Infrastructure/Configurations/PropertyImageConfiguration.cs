using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionRealState.Domain.Aggregates.Property;

namespace MillionRealState.Infrastructure.Configurations
{
    /// <summary>
    /// Configuración de EF Core para la entidad <see cref="PropertyImage"/>.
    /// Define el mapeo de la entidad PropertyImage a la tabla 'PropertyImages' en la base de datos,
    /// incluyendo sus propiedades, restricciones y la relación con la propiedad.
    /// </summary>
    internal class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        /// <summary>
        /// Configura el esquema de la entidad <see cref="PropertyImage"/> para EF Core.
        /// </summary>
        /// <param name="builder">Constructor de la entidad para definir el mapeo.</param>
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            // Nombre de la tabla en plural
            builder.ToTable("PropertyImages");

            // Clave primaria
            builder.HasKey(img => img.IdPropertyImage);

            // Propiedad File: requerida, longitud máxima 250
            builder.Property(img => img.File)
                .IsRequired()
                .HasMaxLength(250);

            // Propiedad Enabled: requerida
            builder.Property(img => img.Enabled)
                .IsRequired();

            // Propiedad IdProperty: requerida (clave foránea)
            builder.Property(img => img.IdProperty)
                .IsRequired();

            // Relación con PropertyAggregate
            builder.HasOne<PropertyAggregate>()
                .WithMany(p => p.Images)
                .HasForeignKey(img => img.IdProperty)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}