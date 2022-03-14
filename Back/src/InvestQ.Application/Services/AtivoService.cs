using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos;
using InvestQ.Application.Dtos.Enum;
using InvestQ.Application.Interfaces;
using InvestQ.Data.Interfaces;
using InvestQ.Domain.Entities;
using InvestQ.Domain.Entities.Enum;

namespace InvestQ.Application.Services
{
    public class AtivoService : IAtivoService
    {
        private readonly IAtivoRepo _ativoRepo;
        private readonly IMapper _mapper;

        public AtivoService(IAtivoRepo ativoRepo,
                            IMapper mapper)
        {
            _ativoRepo = ativoRepo;
            _mapper = mapper;
        }
        public async Task<AtivoDto> AdicionarAtivo(AtivoDto model)
        {
            if (model.Inativo)
                throw new Exception("Não é possível incluir um Ativo já inativo.");
            
            var ativo = _mapper.Map<Ativo>(model);

            if( await _ativoRepo.GetAtivoByIdAsync(ativo.Id) == null)
            {
                _ativoRepo.Adicionar(ativo);

                if (await _ativoRepo.SalvarMudancasAsync())
                {
                    var retorno = await _ativoRepo.GetAtivoByIdsAsync(ativo.Id, ativo.TipoDeAtivo);

                    return _mapper.Map<AtivoDto>(retorno);
                }
            }

            return null;
        }

        public async Task<AtivoDto> AtualizarAtivo(AtivoDto model)
        {
            try
            {
                if (model.Inativo)
                    throw new Exception("Não é possível atualizar um Ativo já inativo.");

                var ativo = await _ativoRepo.GetAtivoByIdAsync(model.Id);

                if (ativo != null)
                {
                    if (ativo.Inativo)
                        throw new Exception("Não se pode alterar um Ativo inativo.");

                    model.Inativo = ativo.Inativo;
                    model.DataDeCriacao = ativo.DataDeCriacao;

                    _mapper.Map(model, ativo);

                    _ativoRepo.Atualizar(ativo);

                    if (await _ativoRepo.SalvarMudancasAsync())
                        return _mapper.Map<AtivoDto>(ativo);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletarAtivo(int ativoId)
        {
            var ativo = await _ativoRepo.GetAtivoByIdAsync(ativoId);

            if (ativo == null)
                throw new Exception("O Ativo que tentou deletar não existe.");

            _ativoRepo.Deletar(ativo);

            return await _ativoRepo.SalvarMudancasAsync();
        }

        public async Task<AtivoDto[]> GetAllAtivosByTipoDeAtivoAsync(TipoDeAtivoDto tipoDeAtivoDto)
        {
            try
            {
                var tipoDeAtivo = _mapper.Map<TipoDeAtivo>(tipoDeAtivoDto);

                var ativos = await _ativoRepo.GetAllAtivosByTipoDeAtivoAsync(tipoDeAtivo);

                if (ativos == null) return null;

                var RetornoDto = _mapper.Map<AtivoDto[]>(ativos);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AtivoDto> GetAtivoByIdAsync(int id)
        {
            try
            {
                var ativo = await _ativoRepo.GetAtivoByIdAsync(id);

                if (ativo == null) return null;

                return _mapper.Map<AtivoDto>(ativo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AtivoDto> GetAtivoByIdsAsync(int id, TipoDeAtivoDto tipoDeAtivoDto)
        {
            try
            {
                var tipoDeAtivo = _mapper.Map<TipoDeAtivo>(tipoDeAtivoDto);
                
                var ativo = await _ativoRepo.GetAtivoByIdsAsync(id, tipoDeAtivo);

                if (ativo == null) return null;

                return = _mapper.Map<AtivoDto>(ativo);  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarAtivo(AtivoDto model)
        {
            if (model != null)
            {
                var ativo = _mapper.Map<Ativo>(model);

                ativo.Inativar();
                _ativoRepo.Atualizar(ativo);
                return await _ativoRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarAtivo(AtivoDto model)
        {
            if (model != null)
            {
                var ativo = _mapper.Map<Ativo>(model);

                ativo.Reativar();
                _ativoRepo.Atualizar(ativo);
                return await _ativoRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}