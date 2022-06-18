using System;
using System.Threading.Tasks;
using InvestQ.Data.Paginacao;
using InvestQ.Domain.Entities.FundosImobiliarios;

namespace InvestQ.Data.Interfaces.FundosImobiliarios
{
    public interface IFundoImobiliarioRepo: IGeralRepo
    {
        Task<PageList<FundoImobiliario>> GetAllFundosImobiliariosAsync(PageParams pageParams);
        Task<FundoImobiliario[]> GetFundosImobliariosBySegmentoAnbimaIdAsync(Guid segmentoAnbimaId);
        Task<FundoImobiliario[]> GetFundosImobliariosByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId);
        Task<FundoImobiliario[]> GetFundosImobliariosByAdministradorDeFundoImobiliarioIdAsync(Guid administradorDeFundoImobiliarioId);
        Task<FundoImobiliario> GetFundoImobiliarioByIdAsync(Guid id);          
        Task<FundoImobiliario> GetFundoImobiliarioByDescricaoAsync(string descricao);
    }
}