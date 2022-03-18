using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos;
using InvestQ.Application.Interfaces;
using InvestQ.Data.Interfaces;
using InvestQ.Domain.Entities;

namespace InvestQ.Application.Services
{
    public class TipoDeInvestimentoService : ITipoDeInvestimentoService
    {
        private readonly ITipoDeInvestimentoRepo _TipoDeInvestimentoRepo;
        private readonly IMapper _mapper;

        public TipoDeInvestimentoService(ITipoDeInvestimentoRepo TipoDeInvestimentoRepo,
                                         IMapper mapper)
        {
            _TipoDeInvestimentoRepo = TipoDeInvestimentoRepo;
            _mapper = mapper;
        }  
        public async Task<TipoDeInvestimentoDto> AdicionarTipoDeInvestimento(TipoDeInvestimentoDto model)
        {            
            var tipoDeInvestimento = _mapper.Map<TipoDeInvestimento>(model);

            if( await _TipoDeInvestimentoRepo.GetTipoDeInvestimentoByIdAsync(tipoDeInvestimento.Id) == null)
            {
                _TipoDeInvestimentoRepo.Adicionar(tipoDeInvestimento);

                if (await _TipoDeInvestimentoRepo.SalvarMudancasAsync())
                {
                    var retorno = await _TipoDeInvestimentoRepo.GetTipoDeInvestimentoByIdAsync(tipoDeInvestimento.Id);

                    return _mapper.Map<TipoDeInvestimentoDto>(retorno);
                }
            }

            return null;
        }

        public async Task<TipoDeInvestimentoDto> AtualizarTipoDeInvestimento(TipoDeInvestimentoDto model)
        {
            try
            {
                var tipoDeInvestimento = await _TipoDeInvestimentoRepo.GetTipoDeInvestimentoByIdAsync(model.Id);

                if (tipoDeInvestimento != null)
                {
                    if (tipoDeInvestimento.Inativo)
                        throw new Exception("Não se pode alterar um Tipo de Investimento inativo.");

                    _mapper.Map(model, tipoDeInvestimento);

                    _TipoDeInvestimentoRepo.Atualizar(tipoDeInvestimento);

                    if (await _TipoDeInvestimentoRepo.SalvarMudancasAsync())
                        return _mapper.Map<TipoDeInvestimentoDto>(tipoDeInvestimento);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarTipoDeInvestimento(Guid tipoDeInvestimentoId)
        {
            var tipoDeInvestimento = await _TipoDeInvestimentoRepo.GetTipoDeInvestimentoByIdAsync(tipoDeInvestimentoId);

            if (tipoDeInvestimento == null)
                throw new Exception("O Tipo de Investimento que tentou deletar não existe.");

            _TipoDeInvestimentoRepo.Deletar(tipoDeInvestimento);

            return await _TipoDeInvestimentoRepo.SalvarMudancasAsync();
        }

        public async Task<TipoDeInvestimentoDto[]> GetAllTiposDeInvestimentosAsync()
        {
            try
            {
                var tiposDeInvestimentos = await _TipoDeInvestimentoRepo.GetAllTipoDeInvestimentoAsync();

                if (tiposDeInvestimentos == null) return null;

                return _mapper.Map<TipoDeInvestimentoDto[]>(tiposDeInvestimentos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TipoDeInvestimentoDto> GetTipoDeInvestimentoByIdAsync(Guid tipoDeInvestimentoId)
        {
            try
            {
                var tipoDeInvestimento = await _TipoDeInvestimentoRepo.GetTipoDeInvestimentoByIdAsync(tipoDeInvestimentoId);

                if (tipoDeInvestimento == null) return null;

                return _mapper.Map<TipoDeInvestimentoDto>(tipoDeInvestimento);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarTipoDeInvestimento(TipoDeInvestimentoDto model)
        {
            if (model != null)
            {
                var tipoDeInvestimento = _mapper.Map<TipoDeInvestimento>(model);

                tipoDeInvestimento.Inativar();
                _TipoDeInvestimentoRepo.Atualizar(tipoDeInvestimento);
                return await _TipoDeInvestimentoRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarTipoDeInvestimento(TipoDeInvestimentoDto model)
        {
            if (model != null)
            {
                var tipoDeInvestimento = _mapper.Map<TipoDeInvestimento>(model);

                tipoDeInvestimento.Reativar();
                _TipoDeInvestimentoRepo.Atualizar(tipoDeInvestimento);
                return await _TipoDeInvestimentoRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}