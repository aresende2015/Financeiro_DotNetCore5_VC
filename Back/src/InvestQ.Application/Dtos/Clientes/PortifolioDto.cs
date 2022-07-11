using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Entities.Clientes;

namespace InvestQ.Application.Dtos.Clientes
{
    public class PortifolioDto
    {
        public Guid Id { get; set; }
        public Guid AtivoId { get; set; }
        public int Quantidade { get; set; }
        public string CodigoDoAtivo { get; set; }        
        public decimal PrecoMedio { get; set; }
        public AtivoDto Ativo { get; set; }
        public Guid CarteiraId { get; set; }
        public CarteiraDto Carteira { get; set; }
    }
}