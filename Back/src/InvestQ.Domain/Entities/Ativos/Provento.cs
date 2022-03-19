using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Enum;

namespace InvestQ.Domain.Entities.Ativos
{
    public class Provento : Entity
    {
        public Provento()
        {
            
        }

        public Provento(Guid id, 
                        DateTime dataCom,
                        DateTime dataEx,
                        Decimal valor,
                        Guid ativoId,
                        TipoDeMovimentacao tipoDeMovimentacao)
        {
            Id = id;
            DataCom = dataCom;
            DataEx = dataEx;
            Valor = valor;
            AtivoId = ativoId;
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

        public DateTime DataCom { get; set; }
        public DateTime DataEx { get; set; }
        public decimal Valor { get; set; }
        public TipoDeMovimentacao TipoDeMovimentacao { get; set; }
        public Guid AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}