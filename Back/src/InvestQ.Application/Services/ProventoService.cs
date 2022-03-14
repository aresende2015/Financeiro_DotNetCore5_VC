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
    public class ProventoService : IProventoService
    {
        private readonly IProventoRepo _proventoRepo;
        private readonly IMapper _mapper;

        public ProventoService(IProventoRepo proventoRepo,
                                  IMapper mapper)
        {
            _proventoRepo = proventoRepo;
            _mapper = mapper;
        } 
        public async Task<ProventoDto> AdicionarProvento(ProventoDto model)
        {
            if (model.Inativo)
                throw new Exception("Não é possível incluir um Provento já inativo.");
            
            var provento = _mapper.Map<Provento>(model);

            if( await _proventoRepo.GetProventoByIdAsync(provento.Id) == null)
            {
                _proventoRepo.Adicionar(provento);

                if (await _proventoRepo.SalvarMudancasAsync())
                {
                    var retorno = await _proventoRepo.GetProventoByIdAsync(provento.Id);

                    return _mapper.Map<ProventoDto>(retorno);
                }
            }

            return null;
        }

        public async Task<ProventoDto> AtualizarProvento(ProventoDto model)
        {
            try
            {
                if (model.Inativo)
                    throw new Exception("Não é possível atualizar um Provento já inativo.");

                var provento = await _proventoRepo.GetProventoByIdAsync(model.Id);

                if (provento != null)
                {
                    if (provento.Inativo)
                        throw new Exception("Não se pode alterar um Provento inativo.");

                    model.Inativo = provento.Inativo;
                    model.DataDeCriacao = provento.DataDeCriacao;

                    _mapper.Map(model, provento);

                    _proventoRepo.Atualizar(provento);

                    if (await _proventoRepo.SalvarMudancasAsync())
                        return _mapper.Map<ProventoDto>(provento);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarProvento(int proventoId)
        {
            var provento = await _proventoRepo.GetProventoByIdAsync(proventoId);

            if (provento == null)
                throw new Exception("O Provento que tentou deletar não existe.");

            _proventoRepo.Deletar(provento);

            return await _proventoRepo.SalvarMudancasAsync();
        }

        public async Task<ProventoDto[]> GetAllProventosByAtivoIdAsync(int ativoId)
        {
            try
            {
                var tiposDeAtivos = await _proventoRepo.GetAllProventosByAtivoIdAsync(ativoId);

                if (tiposDeAtivos == null) return null;

                return _mapper.Map<ProventoDto[]>(tiposDeAtivos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProventoDto> GetProventoByIdAsync(int id)
        {
            try
            {
                var provento = await _proventoRepo.GetProventoByIdAsync(id);

                if (provento == null) return null;

                return _mapper.Map<ProventoDto>(provento);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarProvento(ProventoDto model)
        {
            if (model != null)
            {
                var provento = _mapper.Map<Provento>(model);

                provento.Inativar();
                _proventoRepo.Atualizar(provento);
                return await _proventoRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarProvento(ProventoDto model)
        {
            if (model != null)
            {
                var provento = _mapper.Map<Provento>(model);

                provento.Reativar();
                _proventoRepo.Atualizar(provento);
                return await _proventoRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}