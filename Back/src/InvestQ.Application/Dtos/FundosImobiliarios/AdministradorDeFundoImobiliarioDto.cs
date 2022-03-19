using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos.FundosImobiliarios
{
    public class AdministradorDeFundoImobiliarioDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O campo de {0} deve ter 14 caracteres.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RazaoSocial { get; set; }
        public string Telefone { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public IEnumerable<FundoImobiliarioDto> FundosImobiliarios { get; set; }
    }
}