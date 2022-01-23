using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Mappings;
using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Context
{
    public class InvestQContext : DbContext
    {
        public InvestQContext(DbContextOptions<InvestQContext> options) : base(options)
        {
            
        }

        public DbSet<Corretora> Corretoras { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<ClienteCorretora> ClientesCorretoras { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClienteMap());
            builder.ApplyConfiguration(new ClienteCorretoraMap());

            builder.Entity<Cliente>()
            .HasData(
                new List<Cliente>(){
                    new Cliente(1, "012345678900", "Alex", "Resende", DateTime.Parse("01/01/1980")),
                    new Cliente(2, "987654321000", "Caio", "Silva", DateTime.Parse("01/01/2010"))
                }
            );

            builder.Entity<Corretora>()
            .HasData(
                new List<Corretora>(){
                    new Corretora(1, "Clear"),
                    new Corretora(2, "MyCap")
                }
            );

            builder.Entity<ClienteCorretora>()
            .HasData(
                new List<ClienteCorretora>(){
                    new ClienteCorretora(1,1),
                    new ClienteCorretora(1,2),
                    new ClienteCorretora(2,1)
                }
            );
        }
    }
}