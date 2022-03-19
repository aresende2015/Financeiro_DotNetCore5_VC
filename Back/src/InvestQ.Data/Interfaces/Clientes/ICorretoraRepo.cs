using System;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Clientes;

namespace InvestQ.Data.Interfaces.Clientes
{
    public interface ICorretoraRepo : IGeralRepo
    {
        Task<Corretora[]> GetAllCorretorasAsync(bool includeCliente);  
        Task<Corretora[]> GetAllCorretorasByClienteId(Guid clienteId, bool includeCliente);      
        Task<Corretora> GetCorretoraByIdAsync(Guid id, bool includeCliente);
        Task<Corretora> GetCorretoraByDescricaoAsync(string descricao, bool includeCliente);
    }
}