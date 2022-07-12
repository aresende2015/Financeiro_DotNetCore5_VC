using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Paginacao;
using InvestQ.Domain.Entities.Clientes;

namespace InvestQ.Data.Interfaces.Clientes
{
    public interface ICarteiraRepo : IGeralRepo
    {
        Task<Carteira[]> GetAllCarteirasAsync(int userId, bool includeCliente, bool includeCorretora);  
        Task<Carteira[]> GetAllCarteirasByClienteId(Guid clienteId, bool includeCliente, bool includeCorretora);      
        Task<Carteira[]> GetAllCarteirasByCorretoraId(Guid corretoraId, bool includeCliente, bool includeCorretora);      
        Task<Carteira> GetCarteiraByIdAsync(Guid id, bool includeCliente, bool includeCorretora);
        bool GetPossuiCarteiraByClienteId(Guid clienteId);
    }
}