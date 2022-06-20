using System;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Domain.Entities.FundosImobiliarios
{
    public class FundoImobiliario : Entity
    {
        public FundoImobiliario() 
        {
        }
        public FundoImobiliario(Guid id, 
                                string cnpj, 
                                string nomePregao, 
                                string descricao,
                                DateTime dataDeInicio,
                                DateTime dataDeFim,
                                TipoDeGestao tipoDeGestao,
                                Guid tipoDeInvestimentoId)
        {
            Id = id;
            CNPJ = cnpj;
            NomePregao = nomePregao;
            Descricao = descricao;
            DataDeInicio =dataDeInicio;
            DataDeFim = dataDeFim;
            TipoDeGestao = tipoDeGestao;
            TipoDeInvestimentoId = tipoDeInvestimentoId;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Fundo Imobiliário já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Fundo Imobiliário já estava ativo.");
        }
        public string CNPJ { get; set; }
        public string NomePregao { get; set; }        
        public string Descricao { get; set; }
        public DateTime DataDeInicio { get; set; }
        public DateTime? DataDeFim { get; set; }
        public TipoDeGestao TipoDeGestao {get; set;}
        public Guid TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimento TipoDeInvestimento { get; set; }
        public Guid SegmentoAnbimaId { get; set; }
        public SegmentoAnbima SegmentoAnbima { get; set; }
        public Guid AdministradorDeFundoImobiliarioId { get; set; }
        public AdministradorDeFundoImobiliario AdministradorDeFundoImobiliario { get; set; }
        //public Guid AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}