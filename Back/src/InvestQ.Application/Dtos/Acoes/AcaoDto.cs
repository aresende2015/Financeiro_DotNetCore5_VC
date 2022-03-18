using System;
using System.ComponentModel.DataAnnotations;
using InvestQ.Application.Dtos.Ativos;

namespace InvestQ.Application.Dtos.Acoes
{
    public class AcaoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O campo de {0} deve ter 14 caracteres.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RazaoSocial { get; set; }        
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public int SegmentoId {get; set;}
        public SegmentoDto Segmento { get; set; }
        public int TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimentoDto TipoDeInvestimento { get; set; }
        public int AtivoId { get; set; }
        public AtivoDto Ativo { get; set; }        
    }
}