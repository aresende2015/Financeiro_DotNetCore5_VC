using System;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Data.Interfaces.Acoes
{
    public interface ISetorRepo: IGeralRepo
    {
        Task<Setor[]> GetAllSetoresAsync(bool includeSubsetor);  
        Task<Setor> GetSetorByIdAsync(Guid id, bool includeSubsetor);
        Task<Setor> GetSetorByDescricaoAsync(string descricao, bool includeSubsetor);
    }
}