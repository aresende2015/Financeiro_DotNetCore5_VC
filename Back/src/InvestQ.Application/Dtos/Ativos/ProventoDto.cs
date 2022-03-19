using System;
using InvestQ.Domain.Enum;

namespace InvestQ.Application.Dtos.Ativos
{
    public class ProventoDto
    {
        public Guid Id { get; set; }
        public DateTime DataCom { get; set; }
        public DateTime DataEx { get; set; }
        public decimal Valor { get; set; }
        public TipoDeMovimentacao TipoDeMovimentacao { get; set; }
        public Guid AtivoId { get; set; }
        public AtivoDto Ativo { get; set; }
    }
}