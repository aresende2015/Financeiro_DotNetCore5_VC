using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos.Acoes
{
    public class SegmentoDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo de {0} deve ter no mínimo 3 e no máximo 50 caracteres.")]
        public string Descricao { get; set; }
        public Guid SubsetorId { get; set; }
        public SubsetorDto Subsetor { get; set; }
        public IEnumerable<AcaoDto> Acoes { get; set; }
    }
}