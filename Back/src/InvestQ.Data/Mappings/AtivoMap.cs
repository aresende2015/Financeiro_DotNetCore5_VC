using InvestQ.Domain.Entities.Ativos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class AtivoMap : IEntityTypeConfiguration<Ativo>
    {
        public void Configure(EntityTypeBuilder<Ativo> builder)
        {
            builder.ToTable("Ativos");
            
            builder.Property(a => a.CodigoDoAtivo).IsRequired().HasMaxLength(150);
            builder.HasOne(a => a.Acao).WithOne(a => a.Ativo).IsRequired(false);
            builder.HasOne(a => a.FundoImobiliario).WithOne(a => a.Ativo).IsRequired(false);
            builder.HasOne(a => a.TesouroDireto).WithOne(a => a.Ativo).IsRequired(false);

            builder.HasMany(a => a.Lancamentos)
                    .WithOne(l => l.Ativo)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}