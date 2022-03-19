using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;

namespace InvestQ.Data.Interfaces.Ativos
{
    public interface IProventoRepo: IGeralRepo
    {
        Task<Provento[]> GetAllProventosByAtivoIdAsync(int ativoId);  
        Task<Provento> GetProventoByIdAsync(int id);
    }
}