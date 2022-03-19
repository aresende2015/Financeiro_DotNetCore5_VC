using System.Threading.Tasks;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Data.Interfaces.Acoes
{
    public interface ISetorRepo: IGeralRepo
    {
        Task<Setor[]> GetAllSetoresAsync(bool includeSubsetor);  
        Task<Setor> GetSetorByIdAsync(int id, bool includeSubsetor);
        Task<Setor> GetSetorByDescricaoAsync(string descricao, bool includeSubsetor);
    }
}