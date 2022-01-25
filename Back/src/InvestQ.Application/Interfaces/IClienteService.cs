using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDto> AdicionarCliente(ClienteDto model);
        Task<ClienteDto> AtualizarCliente(int clienteId, ClienteDto model);
        Task<bool> DeletarCliente(int clienteId);
        
        Task<bool> InativarCliente(ClienteDto model);
        Task<bool> ReativarCliente(ClienteDto model);

        Task<ClienteDto[]> GetAllClientesAsync(bool includeCorretora);
        Task<ClienteDto> GetClienteByIdAsync(int clienteId, bool includeCorretora);
        Task<ClienteDto[]> GetAllClientesByCorretoraAsync(int corretoraId, bool includeCorretora);
    }
}