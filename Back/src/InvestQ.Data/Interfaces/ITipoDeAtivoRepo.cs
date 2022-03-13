using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface ITipoDeAtivoRepo: IGeralRepo
    {
        Task<TipoDeAtivo[]> GetAllTiposDeAtivosAsync(bool includeAtivo);  
        Task<TipoDeAtivo> GetTipoDeAtivoByIdAsync(int id, bool includeAtivo);
        Task<TipoDeAtivo> GetTipoDeAtivoByDescricaoAsync(string descricao, bool includeAtivo);
    }
}