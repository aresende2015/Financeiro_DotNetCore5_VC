using System.Threading.Tasks;
using InvestQ.Application.Dtos.Clientes;

namespace InvestQ.Application.Interfaces.Clientes
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
        Task<CorretoraDto> GetCorretoraByDescricaoAsync(string descricao, bool includeCliente);
        Task<CorretoraDto[]> GetAllCorretorasByClienteAsync(int clienteId, bool includeCliente);
    }
}