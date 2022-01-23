using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<Cliente> AdicionarCliente(Cliente model);
        Task<Cliente> AtualizarCliente(Cliente model);
        Task<bool> DeletarCliente(int clienteId);
        
        Task<bool> InativarCliente(Cliente model);
        Task<bool> ReativarCliente(Cliente model);

        Task<Cliente[]> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int clienteId);
    }
}