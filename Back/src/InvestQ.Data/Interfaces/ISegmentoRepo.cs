using System.Threading.Tasks;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Data.Interfaces
{
    public interface ISegmentoRepo: IGeralRepo
    {
        Task<Segmento[]> GetSegmentosBySubsetorIdAsync(int subsetorId);  
        Task<Segmento> GetSegmentoByIdsAsync(int subsetorId, int segmentoId);
    }
}