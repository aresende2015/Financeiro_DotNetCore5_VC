using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
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