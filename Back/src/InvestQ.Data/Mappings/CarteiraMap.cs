using InvestQ.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class CarteiraMap : IEntityTypeConfiguration<Carteira>
    {
        public void Configure(EntityTypeBuilder<Carteira> builder)
        {
            builder.Property(c => c.Descricao)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(c => c.Saldo)
                   .HasColumnType("decimal(20,2)")
                   .IsRequired();

        }
    }
}