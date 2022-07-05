using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Domain.Entities.Clientes
{
    public class Lancamento : Entity
    {
        public Lancamento() 
        {
        }
        public Lancamento(Decimal valorDaOperacao, 
                       DateTime dataDaOperacao, 
                       int quantidade, 
                       TipoDeMovimentacao tipoDeMovimentacao,
                       TipoDeOperacao tipoDeOperacao,
                       Guid ativoId,
                       Guid carteiraId) 
        {
            ValorDaOperacao = valorDaOperacao;
            DataDaOperacao = dataDaOperacao;
            Quantidade = quantidade;
            TipoDeMovimentacao = tipoDeMovimentacao;
            TipoDeOperacao = tipoDeOperacao;
            AtivoId = ativoId;
            CarteiraId = carteiraId;
        }
        public decimal ValorDaOperacao { get; set; }
        public DateTime DataDaOperacao { get; set; }
        public int Quantidade { get; set; }
        public TipoDeMovimentacao TipoDeMovimentacao { get; set; }
        public TipoDeOperacao TipoDeOperacao { get; set; }
        public Guid AtivoId { get; set; }
        public Ativo Ativo { get; set; }
        public Guid CarteiraId { get; set; }
        public Carteira Carteira { get; set; }

    }
}