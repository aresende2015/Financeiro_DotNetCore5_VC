using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface ISetorRepo: IGeralRepo
    {
        Task<Setor[]> GetAllSetoresAsync(bool includeSubsetor);  
        Task<Setor> GetSetorByIdAsync(int id, bool includeSubsetor);
        Task<Setor> GetSetorByDescricaoAsync(string descricao, bool includeSubsetor);
    }
}