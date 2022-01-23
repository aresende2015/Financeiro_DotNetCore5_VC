using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Application.Interfaces
{
    public interface IClienteService
    {
        Task<Cliente> AdicionarCliente(Cliente model);
        Task<Cliente> AtualizarCliente(Cliente model);
        Task<bool> DeletarCliente(int clienteId);
        
        Task<bool> InativarCliente(Cliente model);
        Task<bool> ReativarCliente(Cliente model);

        Task<Cliente[]> GetAllClientesAsync(bool includeCorretora);
        Task<Cliente> GetClienteByIdAsync(int clienteId, bool includeCorretora);
        Task<Cliente[]> GetAllClientesByCorretoraAsync(int corretoraId, bool includeCorretora);
    }
}