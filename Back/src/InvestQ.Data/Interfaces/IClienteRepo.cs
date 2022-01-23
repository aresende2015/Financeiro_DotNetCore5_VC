using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface IClienteRepo : IGeralRepo
    {
        Task<Cliente[]> GetAllClientesAsync(bool includeCorretora = false);  
        Task<Cliente[]> GetAllClientesByCorretoraId(int corretoraId, bool includeCorretora = false);      
        Task<Cliente> GetClienteByIdAsync(int id, bool includeCorretora = false);
        Task<Cliente> GetClienteByCpfAsync(string cpf);
    }
}