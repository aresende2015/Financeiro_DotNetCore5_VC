using System.Threading.Tasks;
using InvestQ.Domain.Entities.FundosImobiliarios;

namespace InvestQ.Data.Interfaces
{
    public interface IAdministradorDeFundoImobiliarioRepo: IGeralRepo
    {
        Task<AdministradorDeFundoImobiliario[]> GetAllAdministradoresDeFundosImobiliariosAsync(bool includeFundoImobiliario);  
        Task<AdministradorDeFundoImobiliario> GetAdministradorDeFundoImobiliarioByIdAsync(int id, bool includeFundoImobiliario);
    }
}