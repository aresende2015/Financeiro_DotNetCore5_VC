using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface IAcaoRepo: IGeralRepo
    {
        Task<Acao[]> GetAllAcoesAsync();
        Task<Acao[]> GetAcoesBySegmentoIdAsync(int segmentoId);
        Task<Acao[]> GetAcoesByTipoDeInvestimentoIdAsync(int tipoDeInvestimentoId);
        Task<Acao> GetAcaoByIdAsync(int id);          
        Task<Acao> GetAcaoByCodigoAsync(string codigo);
    }
}