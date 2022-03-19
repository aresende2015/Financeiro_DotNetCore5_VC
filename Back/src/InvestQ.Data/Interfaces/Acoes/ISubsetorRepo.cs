using System;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Data.Interfaces.Acoes
{
    public interface ISubsetorRepo: IGeralRepo
    {
        Task<Subsetor> GetSubsetorByIdAsync(Guid id);
        Task<Subsetor[]> GetSubsetoresBySetorIdAsync(Guid setorId);  
        Task<Subsetor> GetSubsetorByIdsAsync(Guid setorId, Guid subsetorId);
        
    }
}