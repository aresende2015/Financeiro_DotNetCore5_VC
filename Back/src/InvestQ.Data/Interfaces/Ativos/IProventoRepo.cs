using System;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;

namespace InvestQ.Data.Interfaces.Ativos
{
    public interface IProventoRepo: IGeralRepo
    {
        Task<Provento[]> GetAllProventosAsync();
        Task<Provento[]> GetAllProventosByAtivoIdAsync(Guid ativoId);  
        Task<Provento> GetProventoByIdAsync(Guid id);
    }
}