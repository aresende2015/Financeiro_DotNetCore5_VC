using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface ISubsetorRepo: IGeralRepo
    {
        Task<Subsetor[]> GetAllSubsetoresAsync(bool includeSegmento);  
        Task<Subsetor> GetSubsetorByIdAsync(int id, bool includeSegmento);
        Task<Subsetor> GetSubsetorByDescricaoAsync(string descricao, bool includeSegmento);
        
    }
}