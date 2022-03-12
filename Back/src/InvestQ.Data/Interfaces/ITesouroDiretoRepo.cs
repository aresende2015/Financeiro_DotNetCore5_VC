using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface ITesouroDiretoRepo: IGeralRepo
    {
        Task<TesouroDireto[]> GetAllTeseourosDiretosAsync();
        Task<TesouroDireto[]> GetTeseourosDiretosByTipoDeInvestimentoIdAsync(int tipoDeInvestimentoId);
        Task<TesouroDireto> GetTesouroDiretoByIdAsync(int id);          
        Task<TesouroDireto> GetTesouroDiretoByDescricaoAsync(string descricao);
    }
}