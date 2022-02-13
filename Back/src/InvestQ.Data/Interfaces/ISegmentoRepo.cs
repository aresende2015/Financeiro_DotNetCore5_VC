using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface ISegmentoRepo: IGeralRepo
    {
        Task<Segmento[]> GetSegmentosBySubsetorIdAsync(int subsetorId);  
        Task<Segmento> GetSegmentoByIdsAsync(int subsetorId, int segmentoId);
    }
}