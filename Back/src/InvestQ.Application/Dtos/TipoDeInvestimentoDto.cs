using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InvestQ.Application.Dtos.Acoes;
using InvestQ.Application.Dtos.FundosImobiliarios;
using InvestQ.Application.Dtos.TesourosDiretos;

namespace InvestQ.Application.Dtos
{
    public class TipoDeInvestimentoDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }
        public IEnumerable<TesouroDiretoDto> TesourosDiretos { get; set; }
        public IEnumerable<AcaoDto> Acoes { get; set; }
        public IEnumerable<FundoImobiliarioDto> FundosImobiliarios { get; set; }
    }
}