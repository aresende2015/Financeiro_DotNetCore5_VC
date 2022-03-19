using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Domain.Entities.Acoes
{
    public class Subsetor : Entity
    {
        public Subsetor() 
        {
        }
        public Subsetor(Guid id, 
                        string descricao,
                        Guid setorId)
        {
            Id = id;
            Descricao = descricao;
            SetorId =  setorId;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Subsetor já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Subsetor já estava ativo.");
        }
        public string Descricao { get; set; }
        public Guid SetorId {get; set;}
        public Setor Setor { get; set; }
        public IEnumerable<Segmento> Segmentos { get; set; }
    }
}