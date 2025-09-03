using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MillionRealState.Infrastructure.Data.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            // BasePath dinámico: el proyecto que se use como startup (normalmente el API)
            var basePath = AppContext.BaseDirectory;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("MillionRealStateDb")
                                  ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'MillionRealStateDb'.");

            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new IdentityDbContext(optionsBuilder.Options);
        }
    }
}
