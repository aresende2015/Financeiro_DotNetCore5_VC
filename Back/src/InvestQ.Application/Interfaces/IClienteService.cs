using System.Threading.Tasks;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Data.Paginacao;

namespace InvestQ.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDto> AdicionarCliente(int userId, ClienteDto model);
        Task<ClienteDto> AtualizarCliente(int userId, int clienteId, ClienteDto model);
        Task<bool> DeletarCliente(int userId, int clienteId);
        
        Task<bool> InativarCliente(int userId, ClienteDto model);
        Task<bool> ReativarCliente(int userId, ClienteDto model);

        Task<PageList<ClienteDto>> GetAllClientesAsync(int userId, PageParams pageParams, bool includeCorretora);
        Task<ClienteDto> GetClienteByIdAsync(int userId, int clienteId, bool includeCorretora);
        Task<PageList<ClienteDto>> GetAllClientesByCorretoraAsync(int userId, PageParams pageParams, int corretoraId, bool includeCorretora);
    }
}