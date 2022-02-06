using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Domain.Entities
{
    public class Subsetor
    {
        public Subsetor() 
        {
        }
        public Subsetor(int id, 
                        string descricao,
                        int setorId)
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
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public int SetorId {get; set;}
        public Setor Setor { get; set; }
        public IEnumerable<Segmento> Segmentos { get; set; }
    }
}