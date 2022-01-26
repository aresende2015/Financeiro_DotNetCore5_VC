using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface ICorretoraService
    {
        Task<CorretoraDto> AdicionarCorretora(CorretoraDto model);
        Task<CorretoraDto> AtualizarCorretora(CorretoraDto model);
        Task<bool> DeletarCorretora(int corretoraId);
        
        Task<bool> InativarCorretora(CorretoraDto model);
        Task<bool> ReativarCorretora(CorretoraDto model);

        Task<CorretoraDto[]> GetAllCorretorasAsync(bool includeCliente);
        Task<CorretoraDto> GetCorretoraByIdAsync(int corretoraId, bool includeCliente);
        Task<CorretoraDto[]> GetAllCorretorasByClienteAsync(int clienteId, bool includeCliente);
    }
}