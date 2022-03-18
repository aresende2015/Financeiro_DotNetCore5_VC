using System;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.FundosImobiliarios;
using InvestQ.Application.Interfaces;
using InvestQ.Data.Interfaces;
using InvestQ.Domain.Entities.FundosImobiliarios;

namespace InvestQ.Application.Services
{
    public class SegmentoAnbimaService : ISegmentoAnbimaService
    {
        private readonly ISegmentoAnbimaRepo _segmentoAnbimaRepo;
        private readonly IMapper _mapper;

        public SegmentoAnbimaService(ISegmentoAnbimaRepo segmentoAnbimaRepo,
                                     IMapper mapper)
        {
            _segmentoAnbimaRepo = segmentoAnbimaRepo;
            _mapper = mapper;
        }        
        public async Task<SegmentoAnbimaDto> AdicionarSegmentoAnbima(SegmentoAnbimaDto model)
        {
            if (model.Inativo)
                throw new Exception("Não é possível incluir um Segmento ANBIMA já inativo.");
            
            var segmentoAnbima = _mapper.Map<SegmentoAnbima>(model);

            if (await _segmentoAnbimaRepo.GetSegmentoAnbimaByDescricaoAsync(segmentoAnbima.Descricao, false) != null)
                throw new Exception("Já existe um Segmento ANBIMA com essa descrição.");

            if( await _segmentoAnbimaRepo.GetSegmentoAnbimaByIdAsync(segmentoAnbima.Id, false) == null)
            {
                _segmentoAnbimaRepo.Adicionar(segmentoAnbima);

                if (await _segmentoAnbimaRepo.SalvarMudancasAsync())
                {
                    var retorno = await _segmentoAnbimaRepo.GetSegmentoAnbimaByIdAsync(segmentoAnbima.Id, false);

                    return _mapper.Map<SegmentoAnbimaDto>(retorno);
                }
            }

            return null;
        }

        public async Task<SegmentoAnbimaDto> AtualizarSegmentoAnbima(SegmentoAnbimaDto model)
        {
            try
            {
                if (model.Inativo)
                    throw new Exception("Não é possível atualizar um Segmento ANBIMA já inativo.");

                var segmentoAnbima = await _segmentoAnbimaRepo.GetSegmentoAnbimaByIdAsync(model.Id, false);

                if (segmentoAnbima != null)
                {
                    if (segmentoAnbima.Inativo)
                        throw new Exception("Não se pode alterar um Segmento ANBIMA inativo.");

                    model.Inativo = segmentoAnbima.Inativo;
                    model.DataDeCriacao = segmentoAnbima.DataDeCriacao;

                    _mapper.Map(model, segmentoAnbima);

                    _segmentoAnbimaRepo.Atualizar(segmentoAnbima);

                    if (await _segmentoAnbimaRepo.SalvarMudancasAsync())
                        return _mapper.Map<SegmentoAnbimaDto>(segmentoAnbima);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            }  
        }

        public async Task<bool> DeletarSegmentoAnbima(int segmentoAnbimaId)
        {
            var segmentoAnbima = await _segmentoAnbimaRepo.GetSegmentoAnbimaByIdAsync(segmentoAnbimaId, false);

            if (segmentoAnbima == null)
                throw new Exception("O Segmento ANBIMA que tentou deletar não existe.");

            _segmentoAnbimaRepo.Deletar(segmentoAnbima);

            return await _segmentoAnbimaRepo.SalvarMudancasAsync();
        }

        public async Task<SegmentoAnbimaDto[]> GetAllSegmentosAnbimasAsync(bool includeFundoImobiliario)
        {
            try
            {
                var segmentosAnbimas = await _segmentoAnbimaRepo.GetAllSegmentosAnbimasAsync(includeFundoImobiliario);

                if (segmentosAnbimas == null) return null;

                return _mapper.Map<SegmentoAnbimaDto[]>(segmentosAnbimas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SegmentoAnbimaDto> GetSegmentoAnbimaByDescricaoAsync(string descricao, bool includeFundoImobiliario)
        {
            try
            {
                var segmentoAnbima = await _segmentoAnbimaRepo.GetSegmentoAnbimaByDescricaoAsync(descricao, includeFundoImobiliario);

                if (segmentoAnbima == null) return null;

                return _mapper.Map<SegmentoAnbimaDto>(segmentoAnbima);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SegmentoAnbimaDto> GetSegmentoAnbimaByIdAsync(int segmentoAnbimaId, bool includeFundoImobiliario)
        {
            try
            {
                var segmentoAnbima = await _segmentoAnbimaRepo.GetSegmentoAnbimaByIdAsync(segmentoAnbimaId, includeFundoImobiliario);

                if (segmentoAnbima == null) return null;

                return _mapper.Map<SegmentoAnbimaDto>(segmentoAnbima);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarSegmentoAnbima(SegmentoAnbimaDto model)
        {
            if (model != null)
            {
                var segmentoAnbima = _mapper.Map<SegmentoAnbima>(model);

                segmentoAnbima.Inativar();
                _segmentoAnbimaRepo.Atualizar(segmentoAnbima);
                return await _segmentoAnbimaRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarSegmentoAnbima(SegmentoAnbimaDto model)
        {
            if (model != null)
            {
                var segmentoAnbima = _mapper.Map<SegmentoAnbima>(model);

                segmentoAnbima.Reativar();
                _segmentoAnbimaRepo.Atualizar(segmentoAnbima);
                return await _segmentoAnbimaRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}