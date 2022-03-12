using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface ITesouroDiretoService
    {
        Task<TesouroDiretoDto> AdicionarTesouroDireto(TesouroDiretoDto model);
        Task<TesouroDiretoDto> AtualizarTesouroDireto(TesouroDiretoDto model);
        Task<bool> DeletarTesouroDireto(int tesouroDiretoId);
        
        Task<bool> InativarTesouroDireto(TesouroDiretoDto model);
        Task<bool> ReativarTesouroDireto(TesouroDiretoDto model);
        Task<TesouroDiretoDto[]> GetAllTeseourosDiretosAsync();
        Task<TesouroDiretoDto[]> GetTeseourosDiretosByTipoDeInvestimentoIdAsync(int tipoDeInvestimentoId);
        Task<TesouroDiretoDto> GetTesouroDiretoByIdAsync(int id);          
        Task<TesouroDiretoDto> GetTesouroDiretoByDescricaoAsync(string descricao);
    }
}