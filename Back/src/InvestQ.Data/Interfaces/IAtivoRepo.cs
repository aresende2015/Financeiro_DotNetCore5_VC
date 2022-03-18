using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Data.Interfaces
{
    public interface IAtivoRepo : IGeralRepo
    {
        Task<Ativo[]> GetAllAtivosByTipoDeAtivoAsync(TipoDeAtivo tipoDeAtivo);
        Task<Ativo> GetAtivoByIdAsync(int id);
        Task<Ativo> GetAtivoByIdsAsync(int id, TipoDeAtivo tipoDeAtivo);
    }
}