using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;

namespace InvestQ.Domain.Entities.Clientes
{
    public class Portifolio : Entity
    {
        public Portifolio() 
        {
        }
        public Portifolio(Guid ativoId,
                       int quantidade, 
                       Decimal precoMedio, 
                       Guid carteiraId) 
        {
            AtivoId = ativoId;
            Quantidade = quantidade;
            PrecoMedio = precoMedio;            
            CarteiraId = carteiraId;
        }

        public Guid AtivoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoMedio { get; set; }        
        public Ativo Ativo { get; set; }
        public Guid CarteiraId { get; set; }
        public Carteira Carteira { get; set; }
    }
}