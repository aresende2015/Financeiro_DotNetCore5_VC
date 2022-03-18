using InvestQ.Domain.Entities.Acoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class SegmentoMap : IEntityTypeConfiguration<Segmento>
    {
        public void Configure(EntityTypeBuilder<Segmento> builder)
        {
            builder.ToTable("Segmentos");

            builder.HasMany(s => s.Acoes)
                    .WithOne(a => a.Segmento)
                    .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(s => s.Descricao)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}