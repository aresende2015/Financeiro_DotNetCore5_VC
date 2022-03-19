using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Domain.Entities.Acoes
{
    public class Setor : Entity
    {
        public Setor() 
        {
        }
        public Setor(Guid id, 
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
        public string Descricao { get; set; }
        public IEnumerable<Subsetor> Subsetores { get; set; }
    }
}