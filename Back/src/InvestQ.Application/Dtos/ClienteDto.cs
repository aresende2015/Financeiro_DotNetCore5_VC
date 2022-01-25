using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Application.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public int Idade { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public DateTime? DataDeCriacao { get; set; } = DateTime.Now;
        public bool Inativo { get; set; }  = false;
    }
}