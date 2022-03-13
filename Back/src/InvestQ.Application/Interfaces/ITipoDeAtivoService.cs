using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface ITipoDeAtivoService
    {
        Task<TipoDeAtivoDto> AdicionarTipoDeAtivo(TipoDeAtivoDto model);
        Task<TipoDeAtivoDto> AtualizarTipoDeAtivo(TipoDeAtivoDto model);
        Task<bool> DeletarTipoDeAtivo(int tipoDeAtivoId);
        
        Task<bool> InativarTipoDeAtivo(TipoDeAtivoDto model);
        Task<bool> ReativarTipoDeAtivo(TipoDeAtivoDto model);

        Task<TipoDeAtivoDto[]> GetAllTiposDeAtivosAsync(bool includeAtivo);
        Task<TipoDeAtivoDto> GetTipoDeAtivoByIdAsync(int tipoDeAtivoId, bool includeAtivo);
        Task<TipoDeAtivoDto> GetTipoDeAtivoByDescricaoAsync(string descricao, bool includeAtivo);
    }
}