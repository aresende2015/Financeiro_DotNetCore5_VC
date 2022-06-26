using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos.Clientes
{
    public class CorretoraDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        //[MinLength(3, ErrorMessage = "{0} deve ter no mínimo 3 caracteres.")]
        //[MaxLength(50, ErrorMessage = "{0} deve ter no máximo 50 caracteres.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo de {0} deve ter no mínimo 3 e no máximo 50 caracteres.")]
        public string Descricao { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[RegularExpression(@".*\.(gif|jpe?g|bmp|png|jfif)$",  ErrorMessage = "Não é uma imagem válida. (gif, jpg, jpeg, jfif, bmp ou png")]
        public string Imagen { get; set; }
        public IEnumerable<CarteiraDto> Carteiras { get; set; }
    }
}