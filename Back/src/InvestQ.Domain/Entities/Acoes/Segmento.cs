using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Domain.Entities.Acoes
{
    public class Segmento
    {
        public Segmento() 
        {
        }
        public Segmento(int id, 
                        string descricao,
                        int subsetorId)
        {
            Id = id;
            Descricao = descricao;
            SubsetorId = subsetorId;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Segmento já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Segmento já estava ativo.");
        }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public int SubsetorId {get; set;}
        public Subsetor Subsetor { get; set; }
        public IEnumerable<Acao> Acoes { get; set; }
    }
}