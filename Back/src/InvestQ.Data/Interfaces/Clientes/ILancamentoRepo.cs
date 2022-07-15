using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Paginacao;
using InvestQ.Domain.Entities.Clientes;

namespace InvestQ.Data.Interfaces.Clientes
{
    public interface ILancamentoRepo : IGeralRepo
    {
        Task<PageList<Lancamento>> GetAllLancamentosByCarteiraIdAsync(Guid carteiraId, PageParams pageParams);  
        Task<Lancamento[]> GetAllLancamentosByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId, PageParams pageParams, bool includeCarteira, bool includeAtivo); 
        Task<Lancamento[]> GetAllLancamentosByCarteiraIdAtivoIdSemPaginacaoAsync(Guid carteiraId, Guid ativoId, bool includeCarteira, bool includeAtivo);  
        Task<Lancamento> GetLancamentoByIdAsync(Guid id);
        DateTime GetDataLancamentoByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId);
        bool GetPossuiLancamentoByCarteiraId(Guid carteiraId);

    }
}