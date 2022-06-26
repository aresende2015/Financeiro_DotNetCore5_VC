using InvestQ.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class CarteiraMap : IEntityTypeConfiguration<Carteira>
    {
        public void Configure(EntityTypeBuilder<Carteira> builder)
        {
            builder.HasKey(c => new {c.ClienteId, c.CorretoraId});

            builder.HasOne(c => c.Cliente)
                    .WithMany(cl => cl.Carteiras)
                    .HasForeignKey(c => c.ClienteId)
                    .IsRequired();

            builder.HasOne(c => c.Corretora)
                    .WithMany(co => co.Carteiras)
                    .HasForeignKey(c => c.CorretoraId)
                    .IsRequired();
        }
    }
}