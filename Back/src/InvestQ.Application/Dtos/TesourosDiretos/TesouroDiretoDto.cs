using System;
using System.ComponentModel.DataAnnotations;
using InvestQ.Application.Dtos.Ativos;

namespace InvestQ.Application.Dtos.TesourosDiretos
{
    public class TesouroDiretoDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }
        public DateTime DataDeVencimento { get; set; }
        public bool JurosSemestrais { get; set; }
        public Guid TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimentoDto TipoDeInvestimento { get; set; }
        public Guid AtivoId { get; set; }
        public AtivoDto Ativo { get; set; } 
    }
}