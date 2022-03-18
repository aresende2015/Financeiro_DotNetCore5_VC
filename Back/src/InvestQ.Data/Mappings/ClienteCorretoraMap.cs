using InvestQ.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class ClienteCorretoraMap : IEntityTypeConfiguration<ClienteCorretora>
    {
        public void Configure(EntityTypeBuilder<ClienteCorretora> builder)
        {
            builder.HasKey(cc => new {cc.ClienteId, cc.CorretoraId});

            builder.HasOne(cc => cc.Cliente)
                    .WithMany(cl => cl.ClientesCorretoras)
                    .HasForeignKey(cc => cc.ClienteId)
                    .IsRequired();

            builder.HasOne(cc => cc.Corretora)
                    .WithMany(co => co.ClientesCorretoras)
                    .HasForeignKey(cc => cc.CorretoraId)
                    .IsRequired();
        }
    }
}