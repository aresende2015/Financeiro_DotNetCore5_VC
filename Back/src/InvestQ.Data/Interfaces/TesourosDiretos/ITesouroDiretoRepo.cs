using System.Threading.Tasks;
using InvestQ.Domain.Entities.TesourosDiretos;

namespace InvestQ.Data.Interfaces.TesourosDiretos
{
    public interface ITesouroDiretoRepo: IGeralRepo
    {
        Task<TesouroDireto[]> GetAllTeseourosDiretosAsync();
        Task<TesouroDireto[]> GetTeseourosDiretosByTipoDeInvestimentoIdAsync(int tipoDeInvestimentoId);
        Task<TesouroDireto> GetTesouroDiretoByIdAsync(int id);          
        Task<TesouroDireto> GetTesouroDiretoByDescricaoAsync(string descricao);
    }
}