using System;
using InvestQ.Domain.Entities.Ativos;

namespace InvestQ.Domain.Entities.TesourosDiretos
{
    public class TesouroDireto : Entity
    {
        public TesouroDireto() 
        {
        }
        public TesouroDireto(Guid id, 
                             string descricao,
                             DateTime dataDeVencimento,
                             bool jurosSemestrais,
                             Guid tipoDeInvestimentoId)
        {
            Id                      = id;
            Descricao               = descricao;
            DataDeVencimento        = dataDeVencimento;
            JurosSemestrais         = jurosSemestrais;
            TipoDeInvestimentoId    = tipoDeInvestimentoId;
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
        public DateTime DataDeVencimento { get; set; }
        public bool JurosSemestrais { get; set; }
        public Guid TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimento TipoDeInvestimento { get; set; }
        public Ativo Ativo { get; set; }
    }
}