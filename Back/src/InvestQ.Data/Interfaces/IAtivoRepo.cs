using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;
using InvestQ.Domain.Entities.Enum;

namespace InvestQ.Data.Interfaces
{
    public interface IAtivoRepo : IGeralRepo
    {
        Task<Ativo[]> GetAllAtivosByTipoDeAtivoAsync(TipoDeAtivo tipoDeAtivo);
        Task<Ativo> GetAtivoByIdAsync(int id);
        Task<Ativo> GetAtivoByIdsAsync(int id, TipoDeAtivo tipoDeAtivo);
    }
}