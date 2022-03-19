using System.Threading.Tasks;
using InvestQ.Domain.Entities.FundosImobiliarios;

namespace InvestQ.Data.Interfaces.FundosImobiliarios
{
    public interface IFundoImobiliarioRepo: IGeralRepo
    {
        Task<FundoImobiliario[]> GetAllFundosImobiliariosAsync();
        Task<FundoImobiliario[]> GetFundosImobliariosBySegmentoAnbimaIdAsync(int segmentoAnbimaId);
        Task<FundoImobiliario[]> GetFundosImobliariosByTipoDeInvestimentoIdAsync(int tipoDeInvestimentoId);
        Task<FundoImobiliario[]> GetFundosImobliariosByAdministradorDeFundoImobiliarioIdAsync(int administradorDeFundoImobiliarioId);
        Task<FundoImobiliario> GetFundoImobiliarioByIdAsync(int id);          
        Task<FundoImobiliario> GetFundoImobiliarioByNomePregaoAsync(string nomePregao);
    }
}