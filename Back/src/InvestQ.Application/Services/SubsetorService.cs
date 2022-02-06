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
    public class SubsetorService : ISubsetorService
    {
        private readonly ISubsetorRepo _subsetorRepo;
        private readonly IMapper _mapper;

        public SubsetorService(ISubsetorRepo subsetorRepo,
                               IMapper mapper)
        {
            _subsetorRepo = subsetorRepo;
            _mapper = mapper;
        }
        public async Task<SubsetorDto> AdicionarSubsetor(SubsetorDto model)
        {
            try
            {
                var subsetor = _mapper.Map<Subsetor>(model);

                if (subsetor.Inativo)
                    throw new Exception("Não é possível incluir um Subsetor já inativo.");

                if (await _subsetorRepo.GetSubsetorByDescricaoAsync(subsetor.Descricao, false) != null)
                    throw new Exception("Já existe um Subsetor com esse CPF.");

                if( await _subsetorRepo.GetSubsetorByIdAsync(subsetor.Id,false) == null)
                {
                    _subsetorRepo.Adicionar(subsetor);

                    if (await _subsetorRepo.SalvarMudancasAsync()) {
                        var retorno = await _subsetorRepo.GetSubsetorByIdAsync(subsetor.Id, false);

                        return _mapper.Map<SubsetorDto>(retorno);
                    }
                        
                }

                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<SubsetorDto> AtualizarSubsetor(int subsetorId, SubsetorDto model)
        {
            try
            {
                if (subsetorId != model.Id)
                    throw new Exception("Está tentando alterar o Id errado.");

                if (model.Inativo)
                    throw new Exception("Não é possível atualizar um Subsetor já inativo.");

                var subsetor = await _subsetorRepo.GetSubsetorByIdAsync(subsetorId, false);
                
                if (subsetor != null) 
                {
                    if (subsetor.Inativo)
                        throw new Exception("Não se pode alterar um Subsetor inativo.");

                    model.Inativo = subsetor.Inativo;
                    model.DataDeCriacao = subsetor.DataDeCriacao;

                    _mapper.Map(model, subsetor);

                    _subsetorRepo.Atualizar(subsetor);

                    if (await _subsetorRepo.SalvarMudancasAsync())
                        return _mapper.Map<SubsetorDto>(subsetor);
                }

                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletarSubsetor(int subsetorId)
        {
            var subsetor = await _subsetorRepo.GetSubsetorByIdAsync(subsetorId, false);

            if (subsetor == null)
                throw new Exception("O Subsetor que tentou deletar não existe.");

            _subsetorRepo.Deletar(subsetor);

            return await _subsetorRepo.SalvarMudancasAsync();
        }

        public async Task<SubsetorDto[]> GetAllSubsetoresAsync(bool includeSegmento)
        {
            try
            {
                var subsetores = await _subsetorRepo.GetAllSubsetoresAsync(includeSegmento);

                if (subsetores == null) return null;

                var RetornoDto = _mapper.Map<SubsetorDto[]>(subsetores);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SubsetorDto> GetSubsetorByIdAsync(int subsetorId, bool includeSegmento)
        {
            try
            {
                var subsetor = await _subsetorRepo.GetSubsetorByIdAsync(subsetorId, includeSegmento);

                if (subsetor == null) return null;

                var RetornoDto = _mapper.Map<SubsetorDto>(subsetor);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarSubsetor(SubsetorDto model)
        {
            if (model != null)
            {
                var subsetor = _mapper.Map<Subsetor>(model);

                subsetor.Inativar();
                _subsetorRepo.Atualizar(subsetor);
                return await _subsetorRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarSubsetor(SubsetorDto model)
        {
            if (model != null)
            {
                var subsetor = _mapper.Map<Subsetor>(model);

                subsetor.Reativar();
                _subsetorRepo.Atualizar(subsetor);
                return await _subsetorRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}