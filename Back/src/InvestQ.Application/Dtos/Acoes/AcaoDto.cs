using System;
using System.ComponentModel.DataAnnotations;
using InvestQ.Application.Dtos.Ativos;

namespace InvestQ.Application.Dtos.Acoes
{
    public class AcaoDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O campo de {0} deve ter 14 caracteres.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RazaoSocial { get; set; }        
        public Guid SegmentoId {get; set;}
        public SegmentoDto Segmento { get; set; }
        public Guid TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimentoDto TipoDeInvestimento { get; set; }
        public Guid AtivoId { get; set; }
        public AtivoDto Ativo { get; set; }        
    }
}