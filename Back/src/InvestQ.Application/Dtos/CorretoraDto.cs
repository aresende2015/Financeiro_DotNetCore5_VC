using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos
{
    public class CorretoraDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public bool Inativo { get; set; } 
        public IEnumerable<ClienteCorretoraDto> ClientesCorretoras { get; set; }
    }
}