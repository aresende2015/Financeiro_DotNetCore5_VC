using InvestQ.Domain.Entities.FundosImobiliarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class AdministradorDeFundoImobiliarioMap : IEntityTypeConfiguration<AdministradorDeFundoImobiliario>
    {
        public void Configure(EntityTypeBuilder<AdministradorDeFundoImobiliario> builder)
        {
            builder.ToTable("AdministradoresDeFundosImobiliarios");

            builder.HasMany(af => af.FundosImobiliarios)
                    .WithOne(fi => fi.AdministradorDeFundoImobiliario)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.CNPJ)
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder.Property(a => a.RazaoSocial)
                    .IsRequired()
                    .HasMaxLength(150);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(250);  
        }
    }
}