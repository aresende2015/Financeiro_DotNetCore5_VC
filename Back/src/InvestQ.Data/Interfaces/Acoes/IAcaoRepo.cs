using System;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Data.Interfaces.Acoes
{
    public interface IAcaoRepo: IGeralRepo
    {
        Task<Acao[]> GetAllAcoesAsync();
        Task<Acao[]> GetAcoesBySegmentoIdAsync(Guid segmentoId);
        Task<Acao[]> GetAcoesByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId);
        Task<Acao> GetAcaoByIdAsync(Guid id);          
        Task<Acao> GetAcaoByDescricaoAsync(string descricao);
    }
}