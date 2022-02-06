using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface ISetorService
    {
        Task<SetorDto> AdicionarSetor(SetorDto model);
        Task<SetorDto> AtualizarSetor(int setorId, SetorDto model);
        Task<bool> DeletarSetor(int setorId);
        
        Task<bool> InativarSetor(SetorDto model);
        Task<bool> ReativarSetor(SetorDto model);

        Task<SetorDto[]> GetAllSetoresAsync(bool includeSubsetor);
        Task<SetorDto> GetSetorByIdAsync(int setorId, bool includeSubsetor);
        
    }
}