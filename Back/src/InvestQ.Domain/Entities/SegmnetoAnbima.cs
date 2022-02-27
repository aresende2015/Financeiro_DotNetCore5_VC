using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Enum;

namespace InvestQ.Domain.Entities
{
    public class SegmnetoAnbima
    {
        public SegmnetoAnbima() 
        {
        }
        public SegmnetoAnbima(int id, string descricao, TipoDeGestao tipoDeGestao)
        {
            Id = id;
            Descricao = descricao;
            TipoDeGestao = tipoDeGestao;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Segmento ANBIMA já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Segmento ANBIMA já estava ativo.");
        }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TipoDeGestao TipoDeGestao {get; set;}
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
    }
}