using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos.Clientes
{
    public class CarteiraDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo de {0} deve ter no mínimo 3 e no máximo 50 caracteres.")]
        public string Descricao { get; set; }
        public decimal Saldo { get; set; }
        public string dataDeAtualizadoDoSaldo { get; set; }
        public Guid ClienteId { get; set; }
        public ClienteDto Cliente { get; set; }
        public Guid CorretoraId { get; set; }
        public CorretoraDto Corretora { get; set; }
    }
}