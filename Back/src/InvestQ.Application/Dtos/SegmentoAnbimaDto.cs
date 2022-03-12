using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos
{
    public class SegmentoAnbimaDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo de {0} deve ter no mínimo 3 e no máximo 100 caracteres.")]
        public string Descricao { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public IEnumerable<FundoImobiliarioDto> FundosImobiliarios { get; set; }
    }
}