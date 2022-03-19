using System;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.FundosImobiliarios;

namespace InvestQ.Data.Interfaces.FundosImobiliarios
{
    public interface IAdministradorDeFundoImobiliarioRepo: IGeralRepo
    {
        Task<AdministradorDeFundoImobiliario[]> GetAllAdministradoresDeFundosImobiliariosAsync(bool includeFundoImobiliario);  
        Task<AdministradorDeFundoImobiliario> GetAdministradorDeFundoImobiliarioByIdAsync(Guid id, bool includeFundoImobiliario);
    }
}