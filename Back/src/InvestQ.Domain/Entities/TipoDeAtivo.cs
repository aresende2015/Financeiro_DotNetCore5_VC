using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Domain.Entities
{
    public class TipoDeAtivo
    {
        public TipoDeAtivo()
        {
            
        }

        public TipoDeAtivo(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;            
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Tipo de Ativo já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Tipo de Ativo já estava ativo.");
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        //public IEnumerable<Ativo> Ativos { get; set; }
    }
}