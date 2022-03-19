using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.TesourosDiretos;

namespace InvestQ.Application.Interfaces.TesourosDiretos
{
    public interface ITesouroDiretoService
    {
        Task<TesouroDiretoDto> AdicionarTesouroDireto(TesouroDiretoDto model);
        Task<TesouroDiretoDto> AtualizarTesouroDireto(TesouroDiretoDto model);
        Task<bool> DeletarTesouroDireto(Guid tesouroDiretoId);
        
        Task<bool> InativarTesouroDireto(TesouroDiretoDto model);
        Task<bool> ReativarTesouroDireto(TesouroDiretoDto model);
        Task<TesouroDiretoDto[]> GetAllTeseourosDiretosAsync();
        Task<TesouroDiretoDto[]> GetTeseourosDiretosByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId);
        Task<TesouroDiretoDto> GetTesouroDiretoByIdAsync(Guid id);          
        Task<TesouroDiretoDto> GetTesouroDiretoByDescricaoAsync(string descricao);
    }
}