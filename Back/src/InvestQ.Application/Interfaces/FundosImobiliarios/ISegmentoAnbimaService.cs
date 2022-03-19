using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.FundosImobiliarios;

namespace InvestQ.Application.Interfaces.FundosImobiliarios
{
    public interface ISegmentoAnbimaService
    {
        Task<SegmentoAnbimaDto> AdicionarSegmentoAnbima(SegmentoAnbimaDto model);
        Task<SegmentoAnbimaDto> AtualizarSegmentoAnbima(SegmentoAnbimaDto model);
        Task<bool> DeletarSegmentoAnbima(Guid segmentoAnbimaId);
        
        Task<bool> InativarSegmentoAnbima(SegmentoAnbimaDto model);
        Task<bool> ReativarSegmentoAnbima(SegmentoAnbimaDto model);

        Task<SegmentoAnbimaDto[]> GetAllSegmentosAnbimasAsync(bool includeFundoImobiliario);
        Task<SegmentoAnbimaDto> GetSegmentoAnbimaByIdAsync(Guid segmentoAnbimaId, bool includeFundoImobiliario);
        Task<SegmentoAnbimaDto> GetSegmentoAnbimaByDescricaoAsync(string descricao, bool includeFundoImobiliario);
    }
}