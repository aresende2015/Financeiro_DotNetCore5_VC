using System;
using System.Collections.Generic;
using InvestQ.Domain.Entities.Acoes;
using InvestQ.Domain.Enum;
using InvestQ.Domain.Entities.FundosImobiliarios;
using InvestQ.Domain.Entities.TesourosDiretos;

namespace InvestQ.Domain.Entities.Ativos
{
    public class Ativo
    {
        public Ativo() 
        {
        }
        public Ativo(int id, 
                     TipoDeAtivo tipoDeAtivo)
        {
            Id = id;
            TipoDeAtivo= tipoDeAtivo;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Tipo de Investimento já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Tipo de Investimento já estava ativo.");
        }
        public int Id { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public TipoDeAtivo TipoDeAtivo { get; set; }
        public IEnumerable<Provento> Proventos { get; set; }
        //public virtual int AcaoId {get; set;}
        public virtual Acao Acao { get; set; }
        //public int FundoImobiliarioId {get; set;}
        public virtual FundoImobiliario FundoImobiliario { get; set; }
        //public int TesouroDiretoId {get; set;}
        public virtual TesouroDireto TesouroDireto { get; set; }
    }
}