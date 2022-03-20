using System;
using System.Collections.Generic;
using InvestQ.Domain.Entities.Acoes;
using InvestQ.Domain.Enum;
using InvestQ.Domain.Entities.FundosImobiliarios;
using InvestQ.Domain.Entities.TesourosDiretos;

namespace InvestQ.Domain.Entities.Ativos
{
    public class Ativo : Entity
    {
        public Ativo() 
        {
        }
        public Ativo(Guid id, 
                     Guid acaoId,
                     Guid fundoImobiliarioId,
                     Guid tesourodDiretoId,
                     TipoDeAtivo tipoDeAtivo)
        {
            Id = id;
            AcaoId = acaoId;
            FundoImobiliarioId = fundoImobiliarioId;
            TesouroDiretoId = tesourodDiretoId;
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
        public TipoDeAtivo TipoDeAtivo { get; set; }
        public IEnumerable<Provento> Proventos { get; set; }
        public Guid? AcaoId {get; set;}
        public Acao Acao { get; set; }
        public virtual Guid? FundoImobiliarioId {get; set;}
        public virtual FundoImobiliario FundoImobiliario { get; set; }
        public virtual Guid? TesouroDiretoId {get; set;}
        public virtual TesouroDireto TesouroDireto { get; set; }
    }
}