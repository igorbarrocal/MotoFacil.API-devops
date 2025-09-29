using MotoFacil.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MotoFacil.API.Infrastructure.Mappings;

namespace MotoFacil.API.Infrastructure.Contexts
{
    
    {
        public DbSet<Moto> Motos { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MotoMapping());
        modelBuilder.ApplyConfiguration(new UsuarioMapping());
    }
}
}