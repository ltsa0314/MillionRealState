using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.Aggregates.PropertyTrace;

namespace MillionRealState.Infrastructure.Configurations
{
    /// <summary>
    /// Configuración de EF Core para el agregado <see cref="PropertyTraceAggregate"/>.
    /// </summary>
    internal class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTraceAggregate>
    {
        public void Configure(EntityTypeBuilder<PropertyTraceAggregate> builder)
        {
            builder.ToTable("PropertyTraces");
            builder.HasKey(t => t.IdPropertyTrace);

            builder.Property(t => t.DateSale).IsRequired();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Value).IsRequired();
            builder.Property(t => t.Tax).IsRequired();
            builder.Property(t => t.IdProperty).IsRequired();

            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.CreatedBy).HasMaxLength(100);

            // Relación con PropertyAggregate (solo integridad referencial)
            builder.HasOne<PropertyAggregate>()
                .WithMany(p => p.Traces)
                .HasForeignKey(t => t.IdProperty)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}