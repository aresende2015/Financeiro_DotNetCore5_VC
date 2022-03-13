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
    public class TipoDeAtivoService : ITipoDeAtivoService
    {
        private readonly ITipoDeAtivoRepo _tipoDeAtivoRepo;
        private readonly IMapper _mapper;

        public TipoDeAtivoService(ITipoDeAtivoRepo tipoDeAtivoRepo,
                                  IMapper mapper)
        {
            _tipoDeAtivoRepo = tipoDeAtivoRepo;
            _mapper = mapper;
        } 
        public async Task<TipoDeAtivoDto> AdicionarTipoDeAtivo(TipoDeAtivoDto model)
        {
            if (model.Inativo)
                throw new Exception("Não é possível incluir um Tipo de Ativo já inativo.");
            
            var tipoDeAtivo = _mapper.Map<TipoDeAtivo>(model);

            if (await _tipoDeAtivoRepo.GetTipoDeAtivoByDescricaoAsync(tipoDeAtivo.Descricao, false) != null)
                throw new Exception("Já existe um Tipo de Ativo com essa descrição.");

            if( await _tipoDeAtivoRepo.GetTipoDeAtivoByIdAsync(tipoDeAtivo.Id, false) == null)
            {
                _tipoDeAtivoRepo.Adicionar(tipoDeAtivo);

                if (await _tipoDeAtivoRepo.SalvarMudancasAsync())
                {
                    var retorno = await _tipoDeAtivoRepo.GetTipoDeAtivoByIdAsync(tipoDeAtivo.Id, false);

                    return _mapper.Map<TipoDeAtivoDto>(retorno);
                }
            }

            return null;
        }

        public async Task<TipoDeAtivoDto> AtualizarTipoDeAtivo(TipoDeAtivoDto model)
        {
            try
            {
                if (model.Inativo)
                    throw new Exception("Não é possível atualizar um Tipo de Ativo já inativo.");

                var tipoDeAtivo = await _tipoDeAtivoRepo.GetTipoDeAtivoByIdAsync(model.Id, false);

                if (tipoDeAtivo != null)
                {
                    if (tipoDeAtivo.Inativo)
                        throw new Exception("Não se pode alterar um Tipo de Ativo inativo.");

                    model.Inativo = tipoDeAtivo.Inativo;
                    model.DataDeCriacao = tipoDeAtivo.DataDeCriacao;

                    _mapper.Map(model, tipoDeAtivo);

                    _tipoDeAtivoRepo.Atualizar(tipoDeAtivo);

                    if (await _tipoDeAtivoRepo.SalvarMudancasAsync())
                        return _mapper.Map<TipoDeAtivoDto>(tipoDeAtivo);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarTipoDeAtivo(int tipoDeAtivoId)
        {
            var tipoDeAtivo = await _tipoDeAtivoRepo.GetTipoDeAtivoByIdAsync(tipoDeAtivoId, false);

            if (tipoDeAtivo == null)
                throw new Exception("O Tipo de Ativo que tentou deletar não existe.");

            _tipoDeAtivoRepo.Deletar(tipoDeAtivo);

            return await _tipoDeAtivoRepo.SalvarMudancasAsync();
        }

        public async Task<TipoDeAtivoDto[]> GetAllTiposDeAtivosAsync(bool includeAtivo)
        {
            try
            {
                var tiposDeAtivos = await _tipoDeAtivoRepo.GetAllTiposDeAtivosAsync(includeAtivo);

                if (tiposDeAtivos == null) return null;

                return _mapper.Map<TipoDeAtivoDto[]>(tiposDeAtivos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TipoDeAtivoDto> GetTipoDeAtivoByDescricaoAsync(string descricao, bool includeAtivo)
        {
            try
            {
                var tipoDeAtivo = await _tipoDeAtivoRepo.GetTipoDeAtivoByDescricaoAsync(descricao, includeAtivo);

                if (tipoDeAtivo == null) return null;

                return _mapper.Map<TipoDeAtivoDto>(tipoDeAtivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TipoDeAtivoDto> GetTipoDeAtivoByIdAsync(int tipoDeAtivoId, bool includeAtivo)
        {
            try
            {
                var tipoDeAtivo = await _tipoDeAtivoRepo.GetTipoDeAtivoByIdAsync(tipoDeAtivoId, includeAtivo);

                if (tipoDeAtivo == null) return null;

                return _mapper.Map<TipoDeAtivoDto>(tipoDeAtivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarTipoDeAtivo(TipoDeAtivoDto model)
        {
            if (model != null)
            {
                var tipoDeAtivo = _mapper.Map<TipoDeAtivo>(model);

                tipoDeAtivo.Inativar();
                _tipoDeAtivoRepo.Atualizar(tipoDeAtivo);
                return await _tipoDeAtivoRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarTipoDeAtivo(TipoDeAtivoDto model)
        {
            if (model != null)
            {
                var tipoDeAtivo = _mapper.Map<TipoDeAtivo>(model);

                tipoDeAtivo.Reativar();
                _tipoDeAtivoRepo.Atualizar(tipoDeAtivo);
                return await _tipoDeAtivoRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}