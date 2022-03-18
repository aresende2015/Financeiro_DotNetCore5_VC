using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Domain.Entities.Acoes
{
    public class Setor
    {
        public Setor() 
        {
        }
        public Setor(int id, 
                         string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Setor já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Setor já estava ativo.");
        }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public IEnumerable<Subsetor> Subsetores { get; set; }
    }
}