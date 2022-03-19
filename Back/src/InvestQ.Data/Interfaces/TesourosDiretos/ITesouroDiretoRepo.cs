using System;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.TesourosDiretos;

namespace InvestQ.Data.Interfaces.TesourosDiretos
{
    public interface ITesouroDiretoRepo: IGeralRepo
    {
        Task<TesouroDireto[]> GetAllTeseourosDiretosAsync();
        Task<TesouroDireto[]> GetTeseourosDiretosByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId);
        Task<TesouroDireto> GetTesouroDiretoByIdAsync(Guid id);          
        Task<TesouroDireto> GetTesouroDiretoByDescricaoAsync(string descricao);
    }
}