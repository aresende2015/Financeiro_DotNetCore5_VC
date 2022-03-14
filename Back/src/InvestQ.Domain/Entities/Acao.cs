using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestQ.Domain.Entities
{
    public class Acao
    {
        public Acao() 
        {
        }
        public Acao(int id, 
                    string codigo,
                    string cnpj,
                    string razaoSocial,
                    int segmentoId,
                    int tipoDeInvestimentoId)
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
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }        
        public DateTime DataDeCriacao { get; set; }  = DateTime.Now;
        public bool Inativo { get; set; } = false;
        public int SegmentoId {get; set;}
        public Segmento Segmento { get; set; }
        public int TipoDeInvestimentoId { get; set; }
        public TipoDeInvestimento TipoDeInvestimento { get; set; }
        public int AtivoId { get; set; }
        public Ativo Ativo { get; set; }
    }
}