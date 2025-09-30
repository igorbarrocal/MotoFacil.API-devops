using MotoFacil.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MotoFacil.API.Infrastructure.Mappings
{
    public class MotoMapping : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder)
        {
            builder.ToTable("MOTO");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Modelo).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Placa).IsRequired().HasMaxLength(10);
            builder.Property(m => m.Chassi).IsRequired().HasMaxLength(50);
            builder.Property(m => m.ProblemaIdentificado).HasMaxLength(255);
            builder.HasIndex(m => m.Placa).IsUnique();
            builder.HasIndex(m => m.Chassi).IsUnique();
        }
    }
}