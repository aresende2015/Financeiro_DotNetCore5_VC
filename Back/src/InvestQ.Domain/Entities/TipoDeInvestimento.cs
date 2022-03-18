using System;
using System.Collections.Generic;
using InvestQ.Domain.Entities.Acoes;
using InvestQ.Domain.Entities.FundosImobiliarios;
using InvestQ.Domain.Entities.TesourosDiretos;

namespace InvestQ.Domain.Entities
{
    public class TipoDeInvestimento : Entity
    {
        public TipoDeInvestimento() 
        {
        }
        public TipoDeInvestimento(Guid id, string descricao)
        {
            Id = id;
            Descricao = descricao;
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
        public string Descricao { get; set; }
        public IEnumerable<TesouroDireto> TesourosDiretos { get; set; }
        public IEnumerable<Acao> Acoes { get; set; }
        public IEnumerable<FundoImobiliario> FundosImobiliarios { get; set; }

    }
}