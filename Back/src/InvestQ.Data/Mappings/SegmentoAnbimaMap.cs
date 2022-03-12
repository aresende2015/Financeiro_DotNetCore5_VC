using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class SegmentoAnbimaMap : IEntityTypeConfiguration<SegmentoAnbima>
    {
        public void Configure(EntityTypeBuilder<SegmentoAnbima> builder)
        {
            builder.ToTable("SegmentosAnbimas");

            builder
                .Property(sa => sa.Descricao)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}