using Microsoft.EntityFrameworkCore;
using MillionRealState.Infrastructure.Data.Context;
using MillionRealState.Infrastructure;
using MillionRealState.Application;

namespace MillionRealState.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MillionRealStateDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MillionRealStateDb"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure())
                   .UseLazyLoadingProxies());

            // Register application and infrastructure services
            builder.Services.AddInfrastructure();
            builder.Services.AddApplication();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Aplica migraciones automáticamente al iniciar
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MillionRealStateDbContext>();
                db.Database.EnsureDeleted();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}
