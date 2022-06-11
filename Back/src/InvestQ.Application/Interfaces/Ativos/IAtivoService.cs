using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Dtos.Enum;

namespace InvestQ.Application.Interfaces.Ativos
{
    public interface IAtivoService
    {
        Task<AtivoDto> AdicionarAtivo(AtivoDto model);
        Task<AtivoDto> AtualizarAtivo(AtivoDto model);
        Task<bool> DeletarAtivo(Guid ativoId);
        
        Task<bool> InativarAtivo(AtivoDto model);
        Task<bool> ReativarAtivo(AtivoDto model);
        Task<AtivoDto[]> GetAllAtivosByTipoDeAtivoAsync(TipoDeAtivoDto tipoDeAtivoDto);
        Task<AtivoDto> GetAtivoByTipoDeAtivoDescricaoAsync(TipoDeAtivoDto tipoDeAtivoDto, string descricao);
        Task<AtivoDto> GetAtivoByIdsAsync(Guid id, TipoDeAtivoDto tipoDeAtivoDto);
        Task<AtivoDto> GetAtivoByTesouroDiretoIdAsync(Guid tesouroDiretoId);
        Task<AtivoDto> GetAtivoByIdAsync(Guid id);
    }
}