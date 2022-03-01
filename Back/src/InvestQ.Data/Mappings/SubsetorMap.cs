using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class SubsetorMap : IEntityTypeConfiguration<Subsetor>
    {
        public void Configure(EntityTypeBuilder<Subsetor> builder)
        {
            builder.ToTable("Subsetores");

            builder.HasMany(ss => ss.Segmentos)
                    .WithOne(s => s.Subsetor)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ss => ss.Descricao)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}