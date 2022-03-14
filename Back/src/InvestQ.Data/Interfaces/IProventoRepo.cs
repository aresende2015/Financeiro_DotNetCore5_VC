using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface IProventoRepo: IGeralRepo
    {
        Task<Provento[]> GetAllProventosByAtivoIdAsync(int ativoId);  
        Task<Provento> GetProventoByIdAsync(int id);
    }
}