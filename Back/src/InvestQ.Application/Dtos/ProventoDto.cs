using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using InvestQ.Domain.Entities.Enum;

namespace InvestQ.Application.Dtos
{
    public class ProventoDto
    {
        public int Id { get; set; }
        public DateTime DataCom { get; set; }
        public DateTime DataEx { get; set; }
        public decimal Valor { get; set; }
        public TipoDeMovimentacao TipoDeMovimentacao { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public int AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}