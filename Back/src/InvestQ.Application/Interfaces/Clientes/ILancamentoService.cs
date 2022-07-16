using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Data.Paginacao;

namespace InvestQ.Application.Interfaces.Clientes
{
    public interface ILancamentoService
    {
        Task<LancamentoDto> AdicionarLancamento(LancamentoDto model);
        Task<LancamentoDto> AtualizarLancamento(LancamentoDto model);
        Task<bool> DeletarLancamento(Guid lancamentoId);
        
        Task<PageList<LancamentoDto>> GetAllLancamentosByCarteiraIdAsync(Guid carteiraId, PageParams pageParams);
        Task<LancamentoDto[]> GetAllLancamentosByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId, PageParams pageParams, bool includeCarteira, bool includeAtivo);
        Task<LancamentoDto> GetLancamentoByIdAsync(Guid id, bool includeCarteira, bool includeAtivo);
        bool GetPossuiLancamentosByCarteiraId(Guid carteiraId);
        
    }
}