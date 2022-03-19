using System;
using InvestQ.Domain.Entities.Ativos;

namespace InvestQ.Domain.Entities.Acoes
{
    public class Acao : Entity
    {
        public Acao() 
        {
        }
        public Acao(Guid id, 
                    string codigo,
                    string cnpj,
                    string razaoSocial,
                    Guid segmentoId,
                    Guid tipoDeInvestimentoId)
        {
            Id = id;
            Codigo = codigo;
            CNPJ = cnpj;
            RazaoSocial = razaoSocial;
            SegmentoId = segmentoId;
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
        public string Codigo { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }  
        public Guid SegmentoId {get; set;}
        public Segmento Segmento { get; set; }
        public Guid TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimento TipoDeInvestimento { get; set; }
        public int AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}