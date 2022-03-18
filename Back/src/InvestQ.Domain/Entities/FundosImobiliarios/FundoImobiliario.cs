using System;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Entities.Enum;

namespace InvestQ.Domain.Entities.FundosImobiliarios
{
    public class FundoImobiliario
    {
        public FundoImobiliario() 
        {
        }
        public FundoImobiliario(int id, 
                                string cnpj, 
                                string razaoSocial, 
                                string nomePregao,
                                DateTime dataDeInicio,
                                DateTime dataDeFim,
                                TipoDeGestao tipoDeGestao,
                                int tipoDeInvestimentoId)
        {
            Id = id;
            CNPJ = cnpj;
            RazaoSocial = RazaoSocial;
            NomePregao = NomePregao;
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
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }        
        public string NomePregao { get; set; }
        public DateTime DataDeInicio { get; set; }
        public DateTime DataDeFim { get; set; }
        public TipoDeGestao TipoDeGestao {get; set;}
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public int TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimento TipoDeInvestimento { get; set; }
        public int SegmentoAnbimaId { get; set; }
        public SegmentoAnbima SegmentoAnbima { get; set; }
        public int AdministradorDeFundoImobiliarioId { get; set; }
        public AdministradorDeFundoImobiliario AdministradorDeFundoImobiliario { get; set; }
        public int AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}