using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace MillionRealState.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(assembly);
            });

            // FluentValidation
            services.AddValidatorsFromAssembly(assembly);

            // MediatR - Registrar todos los handlers (commands y queries)
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}
