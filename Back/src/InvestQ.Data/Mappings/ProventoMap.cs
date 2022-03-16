using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class ProventoMap : IEntityTypeConfiguration<Provento>
    {
        public void Configure(EntityTypeBuilder<Provento> builder)
        {
            builder.ToTable("Proventos");

            builder.Property(p => p.Valor)
                   .HasColumnType("decimal(7,2)")
                   .IsRequired();
            
        }
    }
}