using System;
using System.Collections.Generic;

namespace InvestQ.Domain.Entities.Clientes
{
    public class Carteira : Entity
    {
        public Carteira() { }
        public Carteira(Guid clienteId, 
                        Guid corretoraId) 
        {
            this.ClienteId = clienteId;
            this.CorretoraId = corretoraId;   
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"A Carteira já estava inativa.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"A Carteira já estava ativa.");
        }
        public string Descricao { get; set; }
        public decimal Saldo { get; set; }
        public DateTime DataDeAtualizadoDoSaldo { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Guid CorretoraId { get; set; }
        public Corretora Corretora { get; set; }
        public IEnumerable<Lancamento> Lancamentos { get; set; }
    }
}