using Microsoft.Extensions.DependencyInjection;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.Aggregates.PropertyTrace;
using MillionRealState.Infrastructure.Repositories.Write;

namespace MillionRealState.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPropertyReadRepository, PropertyReadRepository>();
            services.AddScoped<IOwnerReadRepository, OwnerReadRepository>();
            services.AddScoped<IPropertyReadTraceRepository, PropertyTraceReadRepository>();

            services.AddScoped<IPropertyWriteRepository, PropertyWriteRepository>();
            services.AddScoped<IOwnerWriteRepository, OwnerWriteRepository>();
            services.AddScoped<IPropertyWriteTraceRepository, PropertyTraceWriteRepository>();

            return services;
        }
    }
}
