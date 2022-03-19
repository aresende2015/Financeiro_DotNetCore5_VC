using System;
using System.Collections.Generic;
namespace InvestQ.Domain.Entities.FundosImobiliarios
{
    public class SegmentoAnbima : Entity
    {
        public SegmentoAnbima() 
        {
        }
        public SegmentoAnbima(Guid id, string descricao)
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
        public string Descricao { get; set; }
        public IEnumerable<FundoImobiliario> FundosImobiliarios { get; set; }
    }
}