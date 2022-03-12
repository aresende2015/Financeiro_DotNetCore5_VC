using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos;

namespace InvestQ.Application.Interfaces
{
    public interface ISegmentoAnbimaService
    {
        Task<SegmentoAnbimaDto> AdicionarSegmentoAnbima(SegmentoAnbimaDto model);
        Task<SegmentoAnbimaDto> AtualizarSegmentoAnbima(SegmentoAnbimaDto model);
        Task<bool> DeletarSegmentoAnbima(int segmentoAnbimaId);
        
        Task<bool> InativarSegmentoAnbima(SegmentoAnbimaDto model);
        Task<bool> ReativarSegmentoAnbima(SegmentoAnbimaDto model);

        Task<SegmentoAnbimaDto[]> GetAllSegmentosAnbimasAsync(bool includeFundoImobiliario);
        Task<SegmentoAnbimaDto> GetSegmentoAnbimaByIdAsync(int segmentoAnbimaId, bool includeFundoImobiliario);
        Task<SegmentoAnbimaDto> GetSegmentoAnbimaByDescricaoAsync(string descricao, bool includeFundoImobiliario);
    }
}