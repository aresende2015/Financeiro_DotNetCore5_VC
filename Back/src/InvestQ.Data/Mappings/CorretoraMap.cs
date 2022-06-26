using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class CorretoraMap : IEntityTypeConfiguration<Corretora>
    {
        public void Configure(EntityTypeBuilder<Corretora> builder)
        {
            builder.ToTable("Corretoras");

            builder.HasMany(c => c.Carteiras)
                    .WithOne(co => co.Corretora)
                    .OnDelete(DeleteBehavior.Cascade);
        }
        
    }
}