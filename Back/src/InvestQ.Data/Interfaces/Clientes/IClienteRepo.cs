using System;
using System.Threading.Tasks;
using InvestQ.Data.Paginacao;
using InvestQ.Domain.Entities.Clientes;

namespace InvestQ.Data.Interfaces.Clientes
{
    public interface IClienteRepo : IGeralRepo
    {
        Task<PageList<Cliente>> GetAllClientesAsync(int userId, PageParams pageParams, bool includeCorretora);  
        Task<PageList<Cliente>> GetAllClientesByCorretoraId(int userId, PageParams pageParams, Guid corretoraId, bool includeCorretora);      
        Task<Cliente> GetClienteByIdAsync(int userId, Guid id, bool includeCorretora);
        Task<Cliente> GetClienteByCpfAsync(int userId, string cpf, bool includeCorretora);
    }
}