using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Acoes;
using InvestQ.Application.Interfaces.Acoes;
using InvestQ.Data.Interfaces.Acoes;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Application.Services.Acoes
{
    public class SegmentoService : ISegmentoService
    {
        private readonly ISegmentoRepo _segmentoRepo;
        private readonly IMapper _mapper;
        public SegmentoService(ISegmentoRepo segmentoRepo,
                               IMapper mapper)
        {
            _segmentoRepo = segmentoRepo;
            _mapper = mapper;
        }

        public async Task AdicionarSegmento(int subsetorId, SegmentoDto model)
        {
            try
            {
                var segmento = _mapper.Map<Segmento>(model);

                segmento.SubsetorId = subsetorId;
                
                _segmentoRepo.Adicionar(segmento);

                await _segmentoRepo.SalvarMudancasAsync();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletarSegmento(int subsetorId, int segmentoId)
        {
            try
            {
                var segmento = await _segmentoRepo.GetSegmentoByIdsAsync(subsetorId, segmentoId);

                if (segmento == null)
                    throw new Exception("O Segmento que tentou deletar não existe.");

                _segmentoRepo.Deletar(segmento);

                return await _segmentoRepo.SalvarMudancasAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SegmentoDto> GetSegmentoByIdsAsync(int subsetorId, int segmentoId)
        {
            try
            {
                var segmento = await _segmentoRepo.GetSegmentoByIdsAsync(subsetorId, segmentoId);

                if (segmento == null) return null;

                var RetornoDto = _mapper.Map<SegmentoDto>(segmento);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SegmentoDto[]> GetSegmentosBySubsetorIdAsync(int subsetorId)
        {
            try
            {
                var segmentos = await _segmentoRepo.GetSegmentosBySubsetorIdAsync(subsetorId);

                if (segmentos == null) return null;

                var RetornoDto = _mapper.Map<SegmentoDto[]>(segmentos);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SegmentoDto> SalvarSegmento(int subsetorId, int segmentoId, SegmentoDto model)
        {
           try
           {
                if ((subsetorId != model.SubsetorId) || (segmentoId != model.Id))
                    throw new Exception("O Segmento não pertece ao subsetorId passado.");

                var segmento = await _segmentoRepo.GetSegmentoByIdsAsync(subsetorId,segmentoId);

                if (segmento != null) 
                {
                    _mapper.Map(model, segmento);

                     _segmentoRepo.Atualizar(segmento);

                    await _segmentoRepo.SalvarMudancasAsync();
                }

                return model;
           }
           catch (Exception ex)
           {
                throw new Exception(ex.Message);
           }
        }

        public async Task<SegmentoDto[]> SalvarSegmentos(int subsetorId, SegmentoDto[] models)
        {
            try
            {
                var segmentos = await _segmentoRepo.GetSegmentosBySubsetorIdAsync(subsetorId);
                
                if (segmentos != null) 
                {

                    foreach (var model in models)
                    {
                        if (model.Id == 0) 
                        {
                            await AdicionarSegmento(subsetorId, model);
                        }
                        else
                        {
                            var segmento = segmentos.FirstOrDefault(s => s.Id == model.Id);
                            
                            model.SubsetorId = subsetorId;

                            _mapper.Map(model, segmento);

                            _segmentoRepo.Atualizar(segmento);

                            await _segmentoRepo.SalvarMudancasAsync();
                        }
                    }

                    var segmentoRetorno = await _segmentoRepo.GetSegmentosBySubsetorIdAsync(subsetorId);

                    return _mapper.Map<SegmentoDto[]>(segmentoRetorno);

                }

                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
    }
}