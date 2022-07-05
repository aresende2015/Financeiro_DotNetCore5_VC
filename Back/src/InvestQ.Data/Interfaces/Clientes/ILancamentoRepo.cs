using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Clientes;

namespace InvestQ.Data.Interfaces.Clientes
{
    public interface ILancamentoRepo : IGeralRepo
    {
        Task<Lancamento[]> GetAllLancamentosByCarteiraIdAsync(Guid carteiraId);  
        Task<Lancamento[]> GetAllLancamentosByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId);  
        Task<Lancamento> GetLancamentoByIdAsync(Guid id);
    }
}