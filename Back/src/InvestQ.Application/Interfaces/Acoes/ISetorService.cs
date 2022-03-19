using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Acoes;

namespace InvestQ.Application.Interfaces.Acoes
{
    public interface ISetorService
    {
        Task<SetorDto> AdicionarSetor(SetorDto model);
        Task<SetorDto> AtualizarSetor(Guid setorId, SetorDto model);
        Task<bool> DeletarSetor(Guid setorId);
        
        Task<bool> InativarSetor(SetorDto model);
        Task<bool> ReativarSetor(SetorDto model);

        Task<SetorDto[]> GetAllSetoresAsync(bool includeSubsetor);
        Task<SetorDto> GetSetorByIdAsync(Guid setorId, bool includeSubsetor);
        
    }
}