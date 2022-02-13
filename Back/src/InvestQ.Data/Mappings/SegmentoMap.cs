using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class SegmentoMap : IEntityTypeConfiguration<Segmento>
    {
        public void Configure(EntityTypeBuilder<Segmento> builder)
        {
            builder.ToTable("Segmentos");

            builder
                .Property(s => s.Descricao)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}