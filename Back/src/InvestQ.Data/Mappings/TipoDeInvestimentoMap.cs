using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class TipoDeInvestimentoMap : IEntityTypeConfiguration<TipoDeInvestimento>
    {
        public void Configure(EntityTypeBuilder<TipoDeInvestimento> builder)
        {
            builder.ToTable("TiposDeInvestimentos");

            builder
                .Property(ti => ti.Descricao)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}