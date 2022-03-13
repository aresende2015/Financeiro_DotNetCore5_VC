using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class TipoDeAtivoMap : IEntityTypeConfiguration<TipoDeAtivo>
    {
        public void Configure(EntityTypeBuilder<TipoDeAtivo> builder)
        {
            builder.ToTable("TiposDeAtivos");

            builder
                .Property(ta => ta.Descricao)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}