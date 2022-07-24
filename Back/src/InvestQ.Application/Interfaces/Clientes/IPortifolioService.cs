using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Application.Dtos.Enum;

namespace InvestQ.Application.Interfaces.Clientes
{
    public interface IPortifolioService
    {
        Task<PortifolioDto> AdicionarPortifolio(PortifolioDto model);
        Task<PortifolioDto> AtualizarPortifolio(PortifolioDto model);
        Task<bool> DeletarPortifolio(Guid portifolioId);
        
        Task<PortifolioDto[]> GetAllPortifoliosByCarteiraIdAsync(Guid carteiraId);
        Task<PortifolioDto[]> GetAllPortifoliosByCarteiraIdTipoDeAtivoAsync(Guid carteiraId, TipoDeAtivoDto tipoDeAtivo);
        Task<PortifolioDto> GetPortifolioByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId);
    }
}