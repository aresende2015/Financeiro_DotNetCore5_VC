using System.Threading.Tasks;
using InvestQ.Application.Dtos.Acoes;

namespace InvestQ.Application.Interfaces
{
    public interface IAcaoService
    {
        Task<AcaoDto> AdicionarAcao(AcaoDto model);
        Task<AcaoDto> AtualizarAcao(AcaoDto model);
        Task<bool> DeletarAcao(int acaoId);
        
        Task<bool> InativarAcao(AcaoDto model);
        Task<bool> ReativarAcao(AcaoDto model);
        Task<AcaoDto[]> GetAllAcoesAsync();
        Task<AcaoDto[]> GetAcoesBySegmentoIdAsync(int segmentoId);
        Task<AcaoDto[]> GetAcoesByTipoDeInvestimentoIdAsync(int tipoDeInvestimentoId);
        Task<AcaoDto> GetAcaoByIdAsync(int id);          
        Task<AcaoDto> GetAcaoByCodigoAsync(string codigo);
    }
}