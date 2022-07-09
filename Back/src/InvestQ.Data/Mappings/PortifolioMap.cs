using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Mappings
{
    public class PortifolioMap : IEntityTypeConfiguration<Portifolio>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Portifolio> builder)
        {
            builder.ToTable("Portifolios");

            builder.Property(p => p.PrecoMedio)
                   .HasColumnType("decimal(20,2)")
                   .IsRequired();
        }
    }
}