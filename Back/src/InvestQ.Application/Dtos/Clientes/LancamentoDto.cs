using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Entities.Clientes;
using InvestQ.Domain.Enum;

namespace InvestQ.Application.Dtos.Clientes
{
    public class LancamentoDto
    {
        public Guid Id { get; set; }
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