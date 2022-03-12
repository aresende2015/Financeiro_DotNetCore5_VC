using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface IAdministradorDeFundoImobiliarioRepo: IGeralRepo
    {
        Task<AdministradorDeFundoImobiliario[]> GetAllAdministradoresDeFundosImobiliariosAsync(bool includeFundoImobiliario);  
        Task<AdministradorDeFundoImobiliario> GetAdministradorDeFundoImobiliarioByIdAsync(int id, bool includeFundoImobiliario);
    }
}