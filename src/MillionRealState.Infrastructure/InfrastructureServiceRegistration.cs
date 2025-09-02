using Microsoft.Extensions.DependencyInjection;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.Aggregates.PropertyTrace;
using MillionRealState.Domain.SeedWork.Contracts;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Repositories;

namespace MillionRealState.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Genérico (si quieres usar BaseRepository para todo):
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));

            // Repos específicos (recomendado cuando hay Includes/consultas especializadas):
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();

            return services;
        }
    }
}
