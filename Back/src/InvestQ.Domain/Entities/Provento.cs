using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Enum;

namespace InvestQ.Domain.Entities
{
    public class Provento
    {
        public Provento()
        {
            
        }

        public Provento(int id, 
                           DateTime dataCom,
                           DateTime dataEx,
                           Decimal valor,
                           TipoDeMovimentacao tipoDeMovimentacao)
        {
            Id = id;
            DataCom = dataCom;
            DataEx = dataEx;
            Valor = valor;
            TipoDeMovimentacao = tipoDeMovimentacao;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Provento já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Provento já estava ativo.");
        }

        public int Id { get; set; }
        public DateTime DataCom { get; set; }
        public DateTime DataEx { get; set; }
        public Decimal Valor { get; set; }
        public TipoDeMovimentacao TipoDeMovimentacao { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public int AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}