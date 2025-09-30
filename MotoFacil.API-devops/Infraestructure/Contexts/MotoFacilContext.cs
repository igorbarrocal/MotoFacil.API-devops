using MotoFacil.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MotoFacil.API.Infrastructure.Contexts
{
    public class MotoFacilContext(DbContextOptions<MotoFacilContext> options) : DbContext(options)
    {
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MotoFacil.API.Infrastructure.Mappings.MotoMapping());
            modelBuilder.ApplyConfiguration(new MotoFacil.API.Infrastructure.Mappings.UsuarioMapping());
        }
    }
}