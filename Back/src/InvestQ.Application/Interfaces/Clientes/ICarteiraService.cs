using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Clientes;

namespace InvestQ.Application.Interfaces.Clientes
{
    public interface ICarteiraService
    {
        Task<CarteiraDto> AdicionarCarteira(CarteiraDto model);
        Task<CarteiraDto> AtualizarCarteira(CarteiraDto model);
        Task<bool> DeletarCarteira(Guid carteiraId);
        
        Task<bool> InativarCarteira(CarteiraDto model);
        Task<bool> ReativarCarteira(CarteiraDto model);

        Task<CarteiraDto[]> GetAllCarteirasAsync(int userId, bool includeCliente, bool includeCorretora);
        Task<CarteiraDto[]> GetAllCarteirasByClienteIdAsync(Guid clienteId, bool includeCliente, bool includeCorretora);
        Task<CarteiraDto[]> GetAllCarteirasByCorretoraIdAsync(Guid corretoraId, bool includeCliente, bool includeCorretora);
        Task<CarteiraDto> GetCarteiraByIdAsync(Guid id, bool includeCliente, bool includeCorretora);        
        
    }
}