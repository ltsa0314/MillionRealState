using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MillionRealState.Application.Abstractions.Services;
using MillionRealState.Application.Features.Properties.Services;
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


            services.AddScoped<IPropertyService, PropertyService>();

            return services;
        }
    }
}
