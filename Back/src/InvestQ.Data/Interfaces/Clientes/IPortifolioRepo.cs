using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Clientes;
using InvestQ.Domain.Enum;

namespace InvestQ.Data.Interfaces.Clientes
{
    public interface IPortifolioRepo : IGeralRepo
    {
        Task<Portifolio[]> GetAllPortifoliosByCarteiraIdAsync(Guid carteiraId);
        Task<Portifolio[]> GetAllPortifoliosByCarteiraIdTipoDeAtivoAsync(Guid carteiraId, TipoDeAtivo tipoDeAtivo);
        Task<Portifolio> GetPortifolioByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId);  
        Task<Portifolio> GetPortifolioByIdAsync(Guid id);
    }
}