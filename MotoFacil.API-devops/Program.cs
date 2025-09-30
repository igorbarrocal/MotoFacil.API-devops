using MotoFacil.API.AutoMapper;
using MotoFacil.API.Infrastructure.Contexts;
using MotoFacil.API.Services;
using MotoFacil.API.Infrastructure.Repositories;
using MotoFacil.API.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace MotoFacil.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<MotoService>();
            builder.Services.AddScoped<IMotoRepository, MotoRepository>();

            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<MotoFacilContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MotoFacil API",
                    Version = "v1",
                    Description = "API para gerenciamento de usuários e motos."
                });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<MotoFacilContext>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Falha ao aplicar migrations no banco de dados.");
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MotoFacil API v1");
                options.RoutePrefix = "swagger";
            });
            
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            
            app.MapControllers();
            app.MapGet("/", () => Results.Redirect("/swagger"));
            app.Run();
        }
    }
}