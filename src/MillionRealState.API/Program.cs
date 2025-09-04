using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MillionRealState.Application;
using MillionRealState.Infrastructure;
using MillionRealState.Infrastructure.Data.Context;
using System.Text;
using Microsoft.OpenApi.Models;

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

            // DbContext  Identity
            builder.Services.AddDbContext<MillionStateIdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MillionRealStateDb")));

            builder.Services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<MillionStateIdentityDbContext>()
                .AddApiEndpoints();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])
                    )
                };
            });

            builder.Services.AddAuthorization();

            // Register application and infrastructure services
            builder.Services.AddInfrastructure();
            builder.Services.AddApplication();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
             {
                 var xmlFilename = "MillionRealState.Api.xml";
                 options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                 options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MillionRealState.Application.xml"));

                 // JWT Bearer Security Definition
                 options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                     Name = "Authorization",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.Http,
                     Scheme = "Bearer",
                     BearerFormat = "JWT"
                 });

                 options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                 });
             });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<MillionRealStateDbContext>();
                db.Database.EnsureDeleted();
                db.Database.Migrate();

                var identityDb = scope.ServiceProvider.GetRequiredService<MillionStateIdentityDbContext>();
                identityDb.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseCors(policy =>
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}

