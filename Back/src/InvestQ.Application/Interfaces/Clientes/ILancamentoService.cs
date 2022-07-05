using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Clientes;

namespace InvestQ.Application.Interfaces.Clientes
{
    public interface ILancamentoService
    {
        Task<LancamentoDto> AdicionarLancamento(LancamentoDto model);
        Task<LancamentoDto> AtualizarLancamento(LancamentoDto model);
        Task<bool> DeletarLancamento(Guid lancamentoId);
        
        Task<LancamentoDto[]> GetAllLancamentosByCarteiraIdAsync(Guid carteiraId);
        Task<LancamentoDto[]> GetAllLancamentosByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId);
        Task<LancamentoDto> GetLancamentoByIdAsync(Guid id);        
        
    }
}