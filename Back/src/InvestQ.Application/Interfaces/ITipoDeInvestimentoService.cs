using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface ITipoDeInvestimentoService
    {
        Task<TipoDeInvestimentoDto> AdicionarTipoDeInvestimento(TipoDeInvestimentoDto model);
        Task<TipoDeInvestimentoDto> AtualizarTipoDeInvestimento(TipoDeInvestimentoDto model);
        Task<bool> DeletarTipoDeInvestimento(int tipoDeInvestimentoId);
        
        Task<bool> InativarTipoDeInvestimento(TipoDeInvestimentoDto model);
        Task<bool> ReativarTipoDeInvestimento(TipoDeInvestimentoDto model);

        Task<TipoDeInvestimentoDto[]> GetAllTiposDeInvestimentosAsync();
        Task<TipoDeInvestimentoDto> GetTipoDeInvestimentoByIdAsync(int tipoDeInvestimentoId);
    }
}