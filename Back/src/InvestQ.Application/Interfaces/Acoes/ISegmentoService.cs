using System;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Acoes;

namespace InvestQ.Application.Interfaces.Acoes
{
    public interface ISegmentoService
    {
        Task<SegmentoDto[]> SalvarSegmentos(Guid subsetorId, SegmentoDto[] models);
        Task<SegmentoDto>SalvarSegmento(Guid subsetorId, Guid segmentoId, SegmentoDto model);
        Task<bool> DeletarSegmento(Guid subsetorId, Guid segmentoId);
        
        //Task<bool> InativarSubsetor(SegmentoDto model);
        //Task<bool> ReativarSubsetor(SegmentoDto model);

        Task<SegmentoDto[]> GetSegmentosBySubsetorIdAsync(Guid subsetorId);
        Task<SegmentoDto> GetSegmentoByIdsAsync(Guid subsetorId, Guid segmentoId);
    }
}