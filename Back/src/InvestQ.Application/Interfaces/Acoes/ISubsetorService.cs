using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Acoes;

namespace InvestQ.Application.Interfaces.Acoes
{
    public interface ISubsetorService
    {
        Task<SubsetorDto[]> SalvarSubsetores(Guid setorId, SubsetorDto[] models);
        Task<bool> DeletarSubsetor(Guid setorId, Guid subsetorId);
        
        //Task<bool> InativarSubsetor(SubsetorDto model);
        //Task<bool> ReativarSubsetor(SubsetorDto model);

        Task<SubsetorDto> GetSubsetorByIdAsync(Guid subsetorId);
        Task<SubsetorDto[]> GetSubsetoresBySetorIdAsync(Guid setorId);
        Task<SubsetorDto> GetSubsetorByIdsAsync(Guid setorId, Guid subsetorId);
        
    }
}