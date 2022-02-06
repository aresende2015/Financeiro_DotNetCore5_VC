using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface ISubsetorService
    {
        Task<SubsetorDto> AdicionarSubsetor(SubsetorDto model);
        Task<SubsetorDto> AtualizarSubsetor(int subsetorId, SubsetorDto model);
        Task<bool> DeletarSubsetor(int subsetorId);
        
        Task<bool> InativarSubsetor(SubsetorDto model);
        Task<bool> ReativarSubsetor(SubsetorDto model);

        Task<SubsetorDto[]> GetAllSubsetoresAsync(bool includeSegmento);
        Task<SubsetorDto> GetSubsetorByIdAsync(int subsetorId, bool includeSegmento);
        
    }
}