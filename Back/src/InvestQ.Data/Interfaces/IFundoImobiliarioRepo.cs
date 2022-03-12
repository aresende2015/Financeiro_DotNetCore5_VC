using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
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