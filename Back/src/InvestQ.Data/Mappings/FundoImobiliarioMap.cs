using InvestQ.Domain.Entities.FundosImobiliarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class FundoImobiliarioMap : IEntityTypeConfiguration<FundoImobiliario>
    {
        public void Configure(EntityTypeBuilder<FundoImobiliario> builder)
        {
            builder.ToTable("FundosImobiliarios");

            builder.Property(fi => fi.CNPJ)
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder.Property(fi => fi.NomePregao)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(fi => fi.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(fi => fi.Ativo).WithOne(a => a.FundoImobiliario).IsRequired();

        }
    }
}