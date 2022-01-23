using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface IClienteRepo : IGeralRepo
    {
        Task<Cliente[]> GetAllClientesAsync(bool includeCorretora);  
        Task<Cliente[]> GetAllClientesByCorretoraId(int corretoraId, bool includeCorretora);      
        Task<Cliente> GetClienteByIdAsync(int id, bool includeCorretora);
        Task<Cliente> GetClienteByCpfAsync(string cpf, bool includeCorretora);
    }
}