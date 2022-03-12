using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class FundoImobiliarioMap : IEntityTypeConfiguration<FundoImobiliario>
    {
        public void Configure(EntityTypeBuilder<FundoImobiliario> builder)
        {
            builder.ToTable("FundosImobiliarios");

            builder.Property(fi => fi.CNPJ)
                .HasColumnType("varchar(14)")
                .IsRequired();

            builder.Property(fi => fi.RazaoSocial)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(fi => fi.NomePregao)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}