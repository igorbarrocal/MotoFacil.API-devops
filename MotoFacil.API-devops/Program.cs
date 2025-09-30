using MotoFacil.API.AutoMapper;
using MotoFacil.API.Infrastructure.Contexts;
using MotoFacil.API.Services;
using MotoFacil.API.Infrastructure.Repositories;
using MotoFacil.API.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MotoFacilContext>();
                db.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}