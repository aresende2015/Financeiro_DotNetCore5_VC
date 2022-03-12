using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Domain.Entities
{
    public class AdministradorDeFundoImobiliario
    {
        public AdministradorDeFundoImobiliario() 
        {
        }
        public AdministradorDeFundoImobiliario(int id,
                                               string cnpj,
                                               string razaoSocial,
                                               string telefone,
                                               string site,
                                               string email)
        {
            Id = id;
            CNPJ = cnpj;
            RazaoSocial = razaoSocial;
            Telefone = telefone;
            Site = site;
            Email = email;      
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Administrador DeF undoImobiliario já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Administrador DeF undoImobiliario já estava ativo.");
        }
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string Telefone { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public IEnumerable<FundoImobiliario> FundosImobiliarios { get; set; }
    }
}