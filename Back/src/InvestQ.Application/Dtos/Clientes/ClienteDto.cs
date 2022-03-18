using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos.Clientes
{
    public class ClienteDto
    {
        public int Id { get; set; }

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
        public DateTime DataDeNascimento { get; set; }
        public DateTime? DataDeCriacao { get; set; } = DateTime.Now;
        public bool Inativo { get; set; }  = false;
        public int UserId { get; set; }
        public UserDto UserDto { get; set; }
        public IEnumerable<ClienteCorretoraDto> ClientesCorretoras { get; set; }
    }
}