using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface ICorretoraRepo : IGeralRepo
    {
        Task<Corretora[]> GetAllCorretorasAsync(bool includeCliente);  
        Task<Corretora[]> GetAllCorretorasByClienteId(int clienteId, bool includeCliente);      
        Task<Corretora> GetCorretoraByIdAsync(int id, bool includeCliente);
        Task<Corretora> GetCorretoraByDescricaoAsync(string descricao, bool includeCliente);
    }
}