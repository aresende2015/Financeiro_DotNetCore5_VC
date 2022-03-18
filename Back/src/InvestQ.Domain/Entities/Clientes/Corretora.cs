using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestQ.Domain.Entities.Clientes
{
    public class Corretora
    {
        public Corretora() 
        {
        }
        public Corretora(int id, 
                         string descricao,
                         string imagem)
        {
            Id = id;
            Descricao = descricao;
            Imagen = imagem;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"A Corretora já estava inativa.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"A Corretora já estava ativa.");
        }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Imagen { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public IEnumerable<ClienteCorretora> ClientesCorretoras { get; set; }
    }
}