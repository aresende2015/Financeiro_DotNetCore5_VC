using InvestQ.Domain.Entities.Ativos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class ProventoMap : IEntityTypeConfiguration<Provento>
    {
        public void Configure(EntityTypeBuilder<Provento> builder)
        {
            builder.ToTable("Proventos");

            builder.Property(p => p.Valor)
                   .HasColumnType("decimal(20,8)")
                   .IsRequired();
            
        }
    }
}