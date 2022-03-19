using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos.Acoes
{
    public class SubsetorDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo de {0} deve ter no mínimo 3 e no máximo 50 caracteres.")]
        public string Descricao { get; set; }
        public Guid SetorId { get; set; }
        public SetorDto Setor { get; set; }
        public IEnumerable<SegmentoDto> Segmentos { get; set; }
        
    }
}