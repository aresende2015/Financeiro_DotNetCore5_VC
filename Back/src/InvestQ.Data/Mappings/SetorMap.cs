using InvestQ.Domain.Entities.Acoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class SetorMap : IEntityTypeConfiguration<Setor>
    {
        public void Configure(EntityTypeBuilder<Setor> builder)
        {
            builder.ToTable("Setores");

            builder.HasMany(s => s.Subsetores)
                    .WithOne(ss => ss.Setor)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.Descricao)
                .IsRequired()
                .HasMaxLength(200);

        }
    }
}