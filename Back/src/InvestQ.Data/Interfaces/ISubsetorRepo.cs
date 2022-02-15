using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface ISubsetorRepo: IGeralRepo
    {
        Task<Subsetor> GetSubsetorByIdAsync(int id);
        Task<Subsetor[]> GetSubsetoresBySetorIdAsync(int setorId);  
        Task<Subsetor> GetSubsetorByIdsAsync(int setorId, int subsetorId);
        
    }
}