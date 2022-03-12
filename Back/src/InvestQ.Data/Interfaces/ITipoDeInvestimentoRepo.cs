using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface ITipoDeInvestimentoRepo: IGeralRepo
    {
        Task<TipoDeInvestimento[]> GetAllTipoDeInvestimentoAsync();  
        Task<TipoDeInvestimento> GetTipoDeInvestimentoByIdAsync(int id);
    }
}