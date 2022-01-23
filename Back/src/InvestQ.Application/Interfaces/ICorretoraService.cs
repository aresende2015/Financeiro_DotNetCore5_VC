using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Application.Interfaces
{
    public interface ICorretoraService
    {
        Task<Corretora> AdicionarCorretora(Corretora model);
        Task<Corretora> AtualizarCorretora(Corretora model);
        Task<bool> DeletarCorretora(int corretoraId);
        
        Task<bool> InativarCorretora(Corretora model);
        Task<bool> ReativarCorretora(Corretora model);

        Task<Corretora[]> GetAllCorretorasAsync();
        Task<Corretora> GetCorretoraByIdAsync(int corretoraId);
    }
}