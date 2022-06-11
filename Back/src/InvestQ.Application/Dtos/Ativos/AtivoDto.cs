using System;
using System.Collections.Generic;
using InvestQ.Application.Dtos.Acoes;
using InvestQ.Application.Dtos.FundosImobiliarios;
using InvestQ.Application.Dtos.TesourosDiretos;
using InvestQ.Domain.Enum;

namespace InvestQ.Application.Dtos.Ativos
{
    public class AtivoDto
    {
        public Guid Id { get; set; }
        //public TipoDeAtivo TipoDeAtivo { get; set; }
        //public IEnumerable<ProventoDto> Proventos { get; set; }
        //public Guid? AcaoId {get; set;}
        //public AcaoDto Acao { get; set; }
        //public Guid? FundoImobiliarioId {get; set;}
        //public FundoImobiliarioDto FundoImobiliario { get; set; }
        public Guid? TesouroDiretoId {get; set;}
        //public TesouroDiretoDto TesouroDireto { get; set; }
    }
}