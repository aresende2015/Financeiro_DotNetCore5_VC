using System;
using System.ComponentModel.DataAnnotations;
using InvestQ.Application.Dtos.Ativos;

namespace InvestQ.Application.Dtos.TesourosDiretos
{
    public class TesouroDiretoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }
        public DateTime DataDeVencimento { get; set; }
        public bool JurosSemestrais { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public int TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimentoDto TipoDeInvestimento { get; set; }
        public int AtivoId { get; set; }
        public AtivoDto Ativo { get; set; } 
    }
}