using System.Threading.Tasks;
using InvestQ.Application.Dtos.Ativos;

namespace InvestQ.Application.Interfaces.Ativos
{
    public interface IProventoService
    {
        Task<ProventoDto> AdicionarProvento(ProventoDto model);
        Task<ProventoDto> AtualizarProvento(ProventoDto model);
        Task<bool> DeletarProvento(int proventoId);
        
        Task<bool> InativarProvento(ProventoDto model);
        Task<bool> ReativarProvento(ProventoDto model);

        Task<ProventoDto[]> GetAllProventosByAtivoIdAsync(int ativoId);
        Task<ProventoDto> GetProventoByIdAsync(int Id);
    }
}