using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;
using InvestQ.Application.Dtos.Enum;

namespace InvestQ.Application.Interfaces
{
    public interface IAtivoService
    {
        Task<AtivoDto> AdicionarAtivo(AtivoDto model);
        Task<AtivoDto> AtualizarAtivo(AtivoDto model);
        Task<bool> DeletarAtivo(int ativoId);
        
        Task<bool> InativarAtivo(AtivoDto model);
        Task<bool> ReativarAtivo(AtivoDto model);
        Task<AtivoDto[]> GetAllAtivosByTipoDeAtivoAsync(TipoDeAtivoDto tipoDeAtivoDto);
        Task<AtivoDto> GetAtivoByIdsAsync(int id, TipoDeAtivoDto tipoDeAtivoDto);
        Task<AtivoDto> GetAtivoByIdAsync(int id);
    }
}