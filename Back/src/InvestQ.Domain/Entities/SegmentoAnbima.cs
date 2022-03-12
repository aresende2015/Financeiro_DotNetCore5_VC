using System;
using System.Collections.Generic;
namespace InvestQ.Domain.Entities
{
    public class SegmentoAnbima
    {
        public SegmentoAnbima() 
        {
        }
        public SegmentoAnbima(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;            
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
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public IEnumerable<FundoImobiliario> FundosImobiliarios { get; set; }
    }
}