using System.Threading.Tasks;
using InvestQ.Application.Dtos.FundosImobiliarios;

namespace InvestQ.Application.Interfaces
{
    public interface IFundoImobiliarioService
    {
        Task<FundoImobiliarioDto> AdicionarFundoImobiliario(FundoImobiliarioDto model);
        Task<FundoImobiliarioDto> AtualizarFundoImobiliario(FundoImobiliarioDto model);
        Task<bool> DeletarFundoImobiliario(int fundoImobiliarioId);
        
        Task<bool> InativarFundoImobiliario(FundoImobiliarioDto model);
        Task<bool> ReativarFundoImobiliario(FundoImobiliarioDto model);

        Task<FundoImobiliarioDto[]> GetAllFundosImobiliariosAsync();
        Task<FundoImobiliarioDto[]> GetFundosImobliariosBySegmentoAnbimaIdAsync(int segmentoAnbimaId);
        Task<FundoImobiliarioDto[]> GetFundosImobliariosByTipoDeInvestimentoIdAsync(int tipoDeInvestimentoId);
        Task<FundoImobiliarioDto[]> GetFundosImobliariosByAdministradorDeFundoImobiliarioIdAsync(int administradorDeFundoImobiliarioId);
        Task<FundoImobiliarioDto> GetFundoImobiliarioByIdAsync(int id);
        Task<FundoImobiliarioDto> GetFundoImobiliarioByNomePregaoAsync(string nomePregao);
    }
}