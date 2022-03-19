using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Acoes;

namespace InvestQ.Application.Interfaces.Acoes
{
    public interface IAcaoService
    {
        Task<AcaoDto> AdicionarAcao(AcaoDto model);
        Task<AcaoDto> AtualizarAcao(AcaoDto model);
        Task<bool> DeletarAcao(Guid acaoId);
        
        Task<bool> InativarAcao(AcaoDto model);
        Task<bool> ReativarAcao(AcaoDto model);
        Task<AcaoDto[]> GetAllAcoesAsync();
        Task<AcaoDto[]> GetAcoesBySegmentoIdAsync(Guid segmentoId);
        Task<AcaoDto[]> GetAcoesByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId);
        Task<AcaoDto> GetAcaoByIdAsync(Guid id);          
        Task<AcaoDto> GetAcaoByCodigoAsync(string codigo);
    }
}