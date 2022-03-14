using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Domain.Entities
{
    public class TesouroDireto
    {
        public TesouroDireto() 
        {
        }
        public TesouroDireto(int id, 
                             string descricao,
                             DateTime dataDeVencimento,
                             bool jurosSemestrais,
                             int tipoDeInvestimentoId)
        {
            Id = id;
            Descricao = descricao;
            DataDeVencimento = dataDeVencimento;
            JurosSemestrais = jurosSemestrais;
            TipoDeInvestimentoId= tipoDeInvestimentoId;
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
        public string Descricao { get; set; }
        public DateTime DataDeVencimento { get; set; }
        public bool JurosSemestrais { get; set; }
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public int TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimento TipoDeInvestimento { get; set; }
        public int AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}