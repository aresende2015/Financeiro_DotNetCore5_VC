using System;
using System.ComponentModel.DataAnnotations;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Application.Dtos.FundosImobiliarios
{
    public class FundoImobiliarioDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O campo de {0} deve ter 14 caracteres.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NomePregao { get; set; }
        public DateTime DataDeInicio { get; set; }
        public DateTime DataDeFim { get; set; }
        public TipoDeGestao TipoDeGestao {get; set;}
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public int TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimentoDto TipoDeInvestimento { get; set; }
        public int SegmentoAnbimaId { get; set; }
        public SegmentoAnbimaDto SegmentoAnbima { get; set; }
        public int AdministradorDeFundoImobiliarioId { get; set; }
        public AdministradorDeFundoImobiliarioDto AdministradorDeFundoImobiliario { get; set; }
        public int AtivoId { get; set; }
        public AtivoDto Ativo { get; set; } 
    }
}