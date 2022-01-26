using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestQ.Domain.Entities
{
    public class Cliente
    {
        public Cliente() 
        {
        }
        public Cliente(int id, 
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
        public int Id { get; set; }        
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public DateTime? DataDeCriacao { get; set; } = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public IEnumerable<ClienteCorretora> ClientesCorretoras { get; set; }
    }
}