using System.Threading.Tasks;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Data.Interfaces
{
    public interface IAcaoRepo: IGeralRepo
    {
        Task<Acao[]> GetAllAcoesAsync();
        Task<Acao[]> GetAcoesBySegmentoIdAsync(int segmentoId);
        Task<Acao[]> GetAcoesByTipoDeInvestimentoIdAsync(int tipoDeInvestimentoId);
        Task<Acao> GetAcaoByIdAsync(int id);          
        Task<Acao> GetAcaoByCodigoAsync(string codigo);
    }
}