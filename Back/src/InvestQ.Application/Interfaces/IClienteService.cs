using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDto> AdicionarCliente(int userId, ClienteDto model);
        Task<ClienteDto> AtualizarCliente(int userId, int clienteId, ClienteDto model);
        Task<bool> DeletarCliente(int userId, int clienteId);
        
        Task<bool> InativarCliente(int userId, ClienteDto model);
        Task<bool> ReativarCliente(int userId, ClienteDto model);

        Task<ClienteDto[]> GetAllClientesAsync(int userId, bool includeCorretora);
        Task<ClienteDto> GetClienteByIdAsync(int userId, int clienteId, bool includeCorretora);
        Task<ClienteDto[]> GetAllClientesByCorretoraAsync(int userId, int corretoraId, bool includeCorretora);
    }
}