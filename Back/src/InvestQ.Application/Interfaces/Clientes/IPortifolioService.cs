using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Clientes;

namespace InvestQ.Application.Interfaces.Clientes
{
    public interface IPortifolioService
    {
        Task<PortifolioDto> AdicionarPortifolio(PortifolioDto model);
        Task<PortifolioDto> AtualizarPortifolio(PortifolioDto model);
        Task<bool> DeletarPortifolio(Guid portifolioId);
        
        Task<PortifolioDto[]> GetAllPortifoliosByCarteiraIdAsync(Guid carteiraId);
        Task<PortifolioDto> GetPortifolioByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId);
    }
}