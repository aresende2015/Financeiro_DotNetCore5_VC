using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InvestQ.Domain.Identity;

namespace InvestQ.Domain.Entities.Clientes
{
    public class Cliente : Entity
    {
        public Cliente() 
        {
        }
        public Cliente(Guid id, 
                       string cpf, 
                       string nome, 
                       string sobreNome,
                       string email,
                       DateTime dataDeNascimento) 
        {
            Id = id;
            Cpf = cpf;
            Nome = nome;
            SobreNome = sobreNome;
            Email = email;
            DataDeNascimento = dataDeNascimento;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Cliente já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Cliente já estava ativo.");
        }    
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Carteira> Carteiras { get; set; }
    }
}