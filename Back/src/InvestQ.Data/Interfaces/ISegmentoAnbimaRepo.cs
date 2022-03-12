using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Interfaces
{
    public interface ISegmentoAnbimaRepo: IGeralRepo
    {
        Task<SegmentoAnbima[]> GetAllSegmentosAnbimasAsync(bool includeFundoImobiliario);  
        Task<SegmentoAnbima> GetSegmentoAnbimaByIdAsync(int id, bool includeFundoImobiliario);
        Task<SegmentoAnbima> GetSegmentoAnbimaByDescricaoAsync(string descricao, bool includeFundoImobiliario);
    }
}