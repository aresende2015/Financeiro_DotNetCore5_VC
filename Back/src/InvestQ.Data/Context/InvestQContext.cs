using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Mappings;
using InvestQ.Domain.Entities;
using InvestQ.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Context
{
    public class InvestQContext : IdentityDbContext<User, Role, int, 
                                                    IdentityUserClaim<int>, 
                                                    UserRole, 
                                                    IdentityUserLogin<int>, 
                                                    IdentityRoleClaim<int>, 
                                                    IdentityUserToken<int>>
    {
        public InvestQContext(DbContextOptions<InvestQContext> options) : base(options)
        {
            
        }

        public DbSet<Corretora> Corretoras { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<ClienteCorretora> ClientesCorretoras { get; set; }

        public DbSet<Setor> Setores { get; set; }

        public DbSet<Subsetor> Subsetores { get; set; }

        public DbSet<Segmento> Segmentos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ClienteMap());
            builder.ApplyConfiguration(new ClienteCorretoraMap());
            builder.ApplyConfiguration(new SetorMap());
            builder.ApplyConfiguration(new SubsetorMap());
            builder.ApplyConfiguration(new SegmentoMap());
            builder.ApplyConfiguration(new UserRoleMap());

            // builder.Entity<Cliente>()
            // .HasData(
            //     new List<Cliente>(){
            //         new Cliente(1, "012345678900", "Alex", "Resende", "alex@gmail.com", DateTime.Parse("01/01/1980")),
            //         new Cliente(2, "987654321000", "Caio", "Silva", "caio@gmail.com", DateTime.Parse("01/01/2010"))
            //     }
            // );

            // builder.Entity<Corretora>()
            // .HasData(
            //     new List<Corretora>(){
            //         new Corretora(1, "Clear", "clear.gif"),
            //         new Corretora(2, "MyCap", "mycap.gif")
            //     }
            // );

            // builder.Entity<ClienteCorretora>()
            // .HasData(
            //     new List<ClienteCorretora>(){
            //         new ClienteCorretora(1,1),
            //         new ClienteCorretora(1,2),
            //         new ClienteCorretora(2,1)
            //     }
            // );

            // builder.Entity<Setor>()
            // .HasData(
            //     new List<Setor>(){
            //         new Setor(1, "Consumo Cíclico"),
            //         new Setor(2, "Consumo não Cíclico")
            //     }
            // );

            // builder.Entity<Subsetor>()
            // .HasData(
            //     new List<Subsetor>(){
            //         new Subsetor(1, "Comércio", 1),
            //         new Subsetor(2, "Viagens e Lazer", 1),
            //         new Subsetor(3, "Alimentos Processados", 2)
            //     }
            // );

            // builder.Entity<Segmento>()
            // .HasData(
            //     new List<Segmento>(){
            //         new Segmento(1, "Produtos Diversos", 1)
            //     }
            // );
        }
    }
}