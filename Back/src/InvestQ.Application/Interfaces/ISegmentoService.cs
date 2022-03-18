using System.Threading.Tasks;
using InvestQ.Application.Dtos.Acoes;

namespace InvestQ.Application.Interfaces
{
    public interface ISegmentoService
    {
        Task<SegmentoDto[]> SalvarSegmentos(int subsetorId, SegmentoDto[] models);
        Task<SegmentoDto>SalvarSegmento(int subsetorId, int segmentoId, SegmentoDto model);
        Task<bool> DeletarSegmento(int subsetorId, int segmentoId);
        
        //Task<bool> InativarSubsetor(SegmentoDto model);
        //Task<bool> ReativarSubsetor(SegmentoDto model);

        Task<SegmentoDto[]> GetSegmentosBySubsetorIdAsync(int subsetorId);
        Task<SegmentoDto> GetSegmentoByIdsAsync(int subsetorId, int segmentoId);
    }
}