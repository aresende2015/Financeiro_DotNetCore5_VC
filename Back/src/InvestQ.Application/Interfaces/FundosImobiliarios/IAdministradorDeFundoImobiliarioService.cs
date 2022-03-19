using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.FundosImobiliarios;

namespace InvestQ.Application.Interfaces.FundosImobiliarios
{
    public interface IAdministradorDeFundoImobiliarioService
    {
        Task<AdministradorDeFundoImobiliarioDto> AdicionarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model);
        Task<AdministradorDeFundoImobiliarioDto> AtualizarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model);
        Task<bool> DeletarAdministradorDeFundoImobiliario(Guid administradorDeFundoImobiliarioId);
        
        Task<bool> InativarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model);
        Task<bool> ReativarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model);

        Task<AdministradorDeFundoImobiliarioDto[]> GetAllAdministradoresDeFundosImobiliariosAsync(bool includeFundoImobiliario);
        Task<AdministradorDeFundoImobiliarioDto> GetAdministradorDeFundoImobiliarioByIdAsync(Guid administradorDeFundoImobiliarioId, bool includeFundoImobiliario);
    }
}