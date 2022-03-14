using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Enum;

namespace InvestQ.Application.Dtos
{
    public class AtivoDto
    {
        public int Id { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public TipoDeAtivo TipoDeAtivo { get; set; }
        public IEnumerable<ProventoDto> Proventos { get; set; }
        //public virtual int AcaoId {get; set;}
        public virtual AcaoDto Acao { get; set; }
        //public int FundoImobiliarioId {get; set;}
        public virtual FundoImobiliarioDto FundoImobiliario { get; set; }
        //public int TesouroDiretoId {get; set;}
        public virtual TesouroDiretoDto TesouroDireto { get; set; }
    }
}