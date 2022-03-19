using System.Threading.Tasks;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Data.Interfaces.Acoes
{
    public interface ISubsetorRepo: IGeralRepo
    {
        Task<Subsetor> GetSubsetorByIdAsync(int id);
        Task<Subsetor[]> GetSubsetoresBySetorIdAsync(int setorId);  
        Task<Subsetor> GetSubsetorByIdsAsync(int setorId, int subsetorId);
        
    }
}