using System;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.FundosImobiliarios;

namespace InvestQ.Data.Interfaces.FundosImobiliarios
{
    public interface IFundoImobiliarioRepo: IGeralRepo
    {
        Task<FundoImobiliario[]> GetAllFundosImobiliariosAsync();
        Task<FundoImobiliario[]> GetFundosImobliariosBySegmentoAnbimaIdAsync(Guid segmentoAnbimaId);
        Task<FundoImobiliario[]> GetFundosImobliariosByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId);
        Task<FundoImobiliario[]> GetFundosImobliariosByAdministradorDeFundoImobiliarioIdAsync(Guid administradorDeFundoImobiliarioId);
        Task<FundoImobiliario> GetFundoImobiliarioByIdAsync(Guid id);          
        Task<FundoImobiliario> GetFundoImobiliarioByNomePregaoAsync(string nomePregao);
    }
}