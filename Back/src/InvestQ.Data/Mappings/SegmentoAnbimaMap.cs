using InvestQ.Domain.Entities.FundosImobiliarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class SegmentoAnbimaMap : IEntityTypeConfiguration<SegmentoAnbima>
    {
        public void Configure(EntityTypeBuilder<SegmentoAnbima> builder)
        {
            builder.ToTable("SegmentosAnbimas");

            builder.HasMany(sa => sa.FundosImobiliarios)
                    .WithOne(fi => fi.SegmentoAnbima)
                    .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(sa => sa.Descricao)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}