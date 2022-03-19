using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.FundosImobiliarios;

namespace InvestQ.Application.Interfaces.FundosImobiliarios
{
    public interface IFundoImobiliarioService
    {
        Task<FundoImobiliarioDto> AdicionarFundoImobiliario(FundoImobiliarioDto model);
        Task<FundoImobiliarioDto> AtualizarFundoImobiliario(FundoImobiliarioDto model);
        Task<bool> DeletarFundoImobiliario(Guid fundoImobiliarioId);
        
        Task<bool> InativarFundoImobiliario(FundoImobiliarioDto model);
        Task<bool> ReativarFundoImobiliario(FundoImobiliarioDto model);

        Task<FundoImobiliarioDto[]> GetAllFundosImobiliariosAsync();
        Task<FundoImobiliarioDto[]> GetFundosImobliariosBySegmentoAnbimaIdAsync(Guid segmentoAnbimaId);
        Task<FundoImobiliarioDto[]> GetFundosImobliariosByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId);
        Task<FundoImobiliarioDto[]> GetFundosImobliariosByAdministradorDeFundoImobiliarioIdAsync(Guid administradorDeFundoImobiliarioId);
        Task<FundoImobiliarioDto> GetFundoImobiliarioByIdAsync(Guid id);
        Task<FundoImobiliarioDto> GetFundoImobiliarioByNomePregaoAsync(string nomePregao);
    }
}