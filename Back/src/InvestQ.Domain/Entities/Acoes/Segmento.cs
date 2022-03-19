using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Domain.Entities.Acoes
{
    public class Segmento : Entity
    {
        public Segmento() 
        {
        }
        public Segmento(Guid id, 
                        string descricao,
                        Guid subsetorId)
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
        public string Descricao { get; set; }
        public Guid SubsetorId {get; set;}
        public Subsetor Subsetor { get; set; }
        public IEnumerable<Acao> Acoes { get; set; }
    }
}