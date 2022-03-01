using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface IClienteRepo : IGeralRepo
    {
        Task<Cliente[]> GetAllClientesAsync(int userId, bool includeCorretora);  
        Task<Cliente[]> GetAllClientesByCorretoraId(int userId, int corretoraId, bool includeCorretora);      
        Task<Cliente> GetClienteByIdAsync(int userId, int id, bool includeCorretora);
        Task<Cliente> GetClienteByCpfAsync(int userId, string cpf, bool includeCorretora);
    }
}