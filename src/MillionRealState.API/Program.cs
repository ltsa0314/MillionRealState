
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
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MillionRealStateDb"))
                   .UseLazyLoadingProxies());


            // Register application and infrastructure services
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
