using System;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Data.Interfaces.Acoes
{
    public interface ISegmentoRepo: IGeralRepo
    {
        Task<Segmento[]> GetSegmentosBySubsetorIdAsync(Guid subsetorId);  
        Task<Segmento> GetSegmentoByIdsAsync(Guid subsetorId, Guid segmentoId);
        Task<Segmento> GetSegmentoByIdAsync(Guid segmentoId);
    }
}