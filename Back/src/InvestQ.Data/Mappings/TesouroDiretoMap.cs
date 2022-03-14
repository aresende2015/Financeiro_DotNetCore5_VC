using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class TesouroDiretoMap : IEntityTypeConfiguration<TesouroDireto>
    {
        public void Configure(EntityTypeBuilder<TesouroDireto> builder)
        {
            builder.ToTable("TesourosDiretos");

            builder
                .Property(td => td.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(td => td.Ativo).WithOne(a => a.TesouroDireto).IsRequired();
        }
    }
}