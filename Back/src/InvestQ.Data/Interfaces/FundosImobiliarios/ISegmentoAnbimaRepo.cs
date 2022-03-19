using System.Threading.Tasks;
using InvestQ.Domain.Entities.FundosImobiliarios;

namespace InvestQ.Data.Interfaces.FundosImobiliarios
{
    public interface ISegmentoAnbimaRepo: IGeralRepo
    {
        Task<SegmentoAnbima[]> GetAllSegmentosAnbimasAsync(bool includeFundoImobiliario);  
        Task<SegmentoAnbima> GetSegmentoAnbimaByIdAsync(int id, bool includeFundoImobiliario);
        Task<SegmentoAnbima> GetSegmentoAnbimaByDescricaoAsync(string descricao, bool includeFundoImobiliario);
    }
}