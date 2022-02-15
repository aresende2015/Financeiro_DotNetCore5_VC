using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface ISubsetorService
    {
        Task<SubsetorDto[]> SalvarSubsetores(int setorId, SubsetorDto[] models);
        Task<bool> DeletarSubsetor(int setorId, int subsetorId);
        
        //Task<bool> InativarSubsetor(SubsetorDto model);
        //Task<bool> ReativarSubsetor(SubsetorDto model);

        Task<SubsetorDto> GetSubsetorByIdAsync(int subsetorId);
        Task<SubsetorDto[]> GetSubsetoresBySetorIdAsync(int setorId);
        Task<SubsetorDto> GetSubsetorByIdsAsync(int setorId, int subsetorId);
        
    }
}