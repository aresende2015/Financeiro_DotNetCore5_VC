using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Data.Paginacao;

namespace InvestQ.Application.Interfaces.Clientes
{
    public interface IClienteService
    {
        Task<ClienteDto> AdicionarCliente(int userId, ClienteDto model);
        Task<ClienteDto> AtualizarCliente(int userId, Guid clienteId, ClienteDto model);
        Task<bool> DeletarCliente(int userId, Guid clienteId);
        
        Task<bool> InativarCliente(int userId, ClienteDto model);
        Task<bool> ReativarCliente(int userId, ClienteDto model);

        Task<PageList<ClienteDto>> GetAllClientesAsync(int userId, PageParams pageParams, bool includeCorretora);
        Task<ClienteDto> GetClienteByIdAsync(int userId, Guid clienteId, bool includeCorretora);
        Task<PageList<ClienteDto>> GetAllClientesByCorretoraAsync(int userId, PageParams pageParams, Guid corretoraId, bool includeCorretora);
    }
}