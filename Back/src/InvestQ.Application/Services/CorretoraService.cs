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
    public class CorretoraService : ICorretoraService
    {
        private readonly ICorretoraRepo _corretoraRepo;
        private readonly IMapper _mapper;

        public CorretoraService(ICorretoraRepo corretoraRepo,
                                IMapper mapper)
        {
            _corretoraRepo = corretoraRepo;
            _mapper = mapper;
        }
        public async Task<CorretoraDto> AdicionarCorretora(CorretoraDto model)
        {
            if (model.Inativo)
                throw new Exception("Não é possível incluir uma Corretora já inativa.");
            
            var corretora = _mapper.Map<Corretora>(model);

            if (await _corretoraRepo.GetCorretoraByDescricaoAsync(corretora.Descricao, false) != null)
                throw new Exception("Já existe uma Corretora com essa descrição.");

            if( await _corretoraRepo.GetCorretoraByIdAsync(corretora.Id, false) == null)
            {
                _corretoraRepo.Adicionar(corretora);

                if (await _corretoraRepo.SalvarMudancasAsync())
                {
                    var retorno = await _corretoraRepo.GetCorretoraByIdAsync(corretora.Id, false);

                    return _mapper.Map<CorretoraDto>(retorno);
                }
            }

            return null;
        }

        public async Task<CorretoraDto> AtualizarCorretora(CorretoraDto model)
        {
            try
            {
                if (model.Inativo)
                                throw new Exception("Não é possível atualizar uma Corretora já inativa.");

                var corretora = await _corretoraRepo.GetCorretoraByIdAsync(model.Id, false);

                if (corretora != null)
                {
                    if (corretora.Inativo)
                        throw new Exception("Não se pode alterar uma Corretora inativa.");

                    model.Inativo = corretora.Inativo;
                    model.DataDeCriacao = corretora.DataDeCriacao;

                    _mapper.Map(model, corretora);

                    _corretoraRepo.Atualizar(corretora);

                    if (await _corretoraRepo.SalvarMudancasAsync())
                        return _mapper.Map<CorretoraDto>(corretora);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            }            
        }

        public async Task<bool> DeletarCorretora(int corretoraId)
        {
            var corretora = await _corretoraRepo.GetCorretoraByIdAsync(corretoraId, false);

            if (corretora == null)
                throw new Exception("A Corretora que tentou deletar não existe.");

            _corretoraRepo.Deletar(corretora);

            return await _corretoraRepo.SalvarMudancasAsync();
        }

        public async Task<CorretoraDto[]> GetAllCorretorasAsync(bool includeCliente = false)
        {
            try
            {
                var corretoras = await _corretoraRepo.GetAllCorretorasAsync(includeCliente);

                if (corretoras == null) return null;

                return _mapper.Map<CorretoraDto[]>(corretoras)     ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CorretoraDto[]> GetAllCorretorasByClienteAsync(int clienteId, bool includeCliente)
        {
            try
            {
                var corretoras = await _corretoraRepo.GetAllCorretorasByClienteId(clienteId, includeCliente);

                if (corretoras == null) return null;

                return _mapper.Map<CorretoraDto[]>(corretoras);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CorretoraDto> GetCorretoraByDescricaoAsync(string descricao, bool includeCliente)
        {
            try
            {
                var corretora = await _corretoraRepo.GetCorretoraByDescricaoAsync(descricao, includeCliente);

                if (corretora == null) return null;

                return _mapper.Map<CorretoraDto>(corretora);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CorretoraDto> GetCorretoraByIdAsync(int corretoraId, bool includeCliente = false)
        {
            try
            {
                var corretora = await _corretoraRepo.GetCorretoraByIdAsync(corretoraId, includeCliente);

                if (corretora == null) return null;

                return _mapper.Map<CorretoraDto>(corretora);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarCorretora(CorretoraDto model)
        {
            if (model != null)
            {
                var corretora = _mapper.Map<Corretora>(model);

                corretora.Inativar();
                _corretoraRepo.Atualizar(corretora);
                return await _corretoraRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarCorretora(CorretoraDto model)
        {
            if (model != null)
            {
                var corretora = _mapper.Map<Corretora>(model);

                corretora.Reativar();
                _corretoraRepo.Atualizar(corretora);
                return await _corretoraRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}