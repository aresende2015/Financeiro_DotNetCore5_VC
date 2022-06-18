using System;
using System.ComponentModel.DataAnnotations;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Application.Dtos.FundosImobiliarios
{
    public class FundoImobiliarioDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O campo de {0} deve ter 14 caracteres.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }
        public string DataDeInicio { get; set; }
        public string DataDeFim { get; set; }
        public TipoDeGestao TipoDeGestao {get; set;}
        public Guid TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimentoDto TipoDeInvestimento { get; set; }
        public Guid SegmentoAnbimaId { get; set; }
        public SegmentoAnbimaDto SegmentoAnbima { get; set; }
        public Guid AdministradorDeFundoImobiliarioId { get; set; }
        public AdministradorDeFundoImobiliarioDto AdministradorDeFundoImobiliario { get; set; }
        public AtivoDto Ativo { get; set; } 
    }
}