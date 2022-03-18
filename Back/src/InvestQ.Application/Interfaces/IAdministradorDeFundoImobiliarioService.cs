using System.Threading.Tasks;
using InvestQ.Application.Dtos.FundosImobiliarios;

namespace InvestQ.Application.Interfaces
{
    public interface IAdministradorDeFundoImobiliarioService
    {
        Task<AdministradorDeFundoImobiliarioDto> AdicionarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model);
        Task<AdministradorDeFundoImobiliarioDto> AtualizarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model);
        Task<bool> DeletarAdministradorDeFundoImobiliario(int administradorDeFundoImobiliarioId);
        
        Task<bool> InativarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model);
        Task<bool> ReativarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model);

        Task<AdministradorDeFundoImobiliarioDto[]> GetAllAdministradoresDeFundosImobiliariosAsync(bool includeFundoImobiliario);
        Task<AdministradorDeFundoImobiliarioDto> GetAdministradorDeFundoImobiliarioByIdAsync(int administradorDeFundoImobiliarioId, bool includeFundoImobiliario);
    }
}