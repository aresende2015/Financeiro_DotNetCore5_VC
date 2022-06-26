using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InvestQ.Application.Dtos.Identity;

namespace InvestQ.Application.Dtos.Clientes
{
    public class ClienteDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo de {0} deve ter 11 caracteres.")]
        public string Cpf { get; set; }

        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string SobreNome { get; set; }

         [EmailAddress(ErrorMessage = "O campo {0} precisa ser um e-email válido."),
         Required(ErrorMessage = "O campo {0} é obrigatório"),
         Display(Name = "e-mail")]
        public string Email { get; set; }

        public int Idade { get; set; }
        public string DataDeNascimento { get; set; }
        public int UserId { get; set; }
        public UserDto UserDto { get; set; }
        public IEnumerable<CarteiraDto> Carteiras { get; set; }
    }
}