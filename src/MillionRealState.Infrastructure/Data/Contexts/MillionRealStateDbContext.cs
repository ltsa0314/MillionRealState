using Microsoft.EntityFrameworkCore;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.Aggregates.PropertyTrace;

namespace MillionRealState.Infrastructure.Data.Context
{
    /// <summary>
    /// DbContext principal para la aplicación MillionRealState.
    /// Incluye los agregados principales del dominio.
    /// </summary>
    public class MillionRealStateDbContext : DbContext
    {
        public MillionRealStateDbContext(DbContextOptions<MillionRealStateDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Propietarios.
        /// </summary>
        public DbSet<OwnerAggregate> Owners { get; set; } = default!;

        /// <summary>
        /// Propiedades.
        /// </summary>
        public DbSet<PropertyAggregate> Properties { get; set; } = default!;

        /// <summary>
        /// Imágenes de propiedades.
        /// </summary>
        public DbSet<PropertyImage> PropertyImages { get; set; } = default!;

        /// <summary>
        /// Trazas de propiedades.
        /// </summary>
        public DbSet<PropertyTraceAggregate> PropertyTraces { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica todas las configuraciones de entidades del ensamblado actual
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MillionRealStateDbContext).Assembly);
        }
    }
}