using InvestQ.Domain.Entities.Acoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class AcaoMap : IEntityTypeConfiguration<Acao>
    {
        public void Configure(EntityTypeBuilder<Acao> builder)
        {
            builder.ToTable("Acoes");

            builder
                .Property(a => a.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.CNPJ)
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder.Property(a => a.RazaoSocial)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasOne(a => a.Ativo).WithOne(a => a.Acao).IsRequired();
        }
    }
}