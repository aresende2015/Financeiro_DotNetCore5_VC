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
    public class AdministradorDeFundoImobiliarioService : IAdministradorDeFundoImobiliarioService
    {
        private readonly IAdministradorDeFundoImobiliarioRepo _administradorDeFundoImobiliarioRepo;
        private readonly IMapper _mapper;

        public AdministradorDeFundoImobiliarioService(
                        IAdministradorDeFundoImobiliarioRepo administradorDeFundoImobiliarioRepo,
                        IMapper mapper)
        {
            _administradorDeFundoImobiliarioRepo = administradorDeFundoImobiliarioRepo;
            _mapper = mapper;
        }
        public async Task<AdministradorDeFundoImobiliarioDto> AdicionarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model)
        {
            if (model.Inativo)
                throw new Exception("Não é possível incluir um Administrador de Fundo Imobiliário já inativo.");
            
            var administradorDeFundoImobiliario = _mapper.Map<AdministradorDeFundoImobiliario>(model);

            if( await _administradorDeFundoImobiliarioRepo.GetAdministradorDeFundoImobiliarioByIdAsync(administradorDeFundoImobiliario.Id, false) == null)
            {
                _administradorDeFundoImobiliarioRepo.Adicionar(administradorDeFundoImobiliario);

                if (await _administradorDeFundoImobiliarioRepo.SalvarMudancasAsync())
                {
                    var retorno = await _administradorDeFundoImobiliarioRepo.GetAdministradorDeFundoImobiliarioByIdAsync(administradorDeFundoImobiliario.Id, false);

                    return _mapper.Map<AdministradorDeFundoImobiliarioDto>(retorno);
                }
            }

            return null;
        }

        public async Task<AdministradorDeFundoImobiliarioDto> AtualizarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model)
        {
            try
            {
                if (model.Inativo)
                    throw new Exception("Não é possível atualizar um Administrador de Fundo Imobiliario já inativo.");

                var administradorDeFundoImobiliario = await _administradorDeFundoImobiliarioRepo.GetAdministradorDeFundoImobiliarioByIdAsync(model.Id, false);

                if (administradorDeFundoImobiliario != null)
                {
                    if (administradorDeFundoImobiliario.Inativo)
                        throw new Exception("Não se pode alterar um Administrador de Fundo Imobiliario inativo.");

                    model.Inativo = administradorDeFundoImobiliario.Inativo;
                    model.DataDeCriacao = administradorDeFundoImobiliario.DataDeCriacao;

                    _mapper.Map(model, administradorDeFundoImobiliario);

                    _administradorDeFundoImobiliarioRepo.Atualizar(administradorDeFundoImobiliario);

                    if (await _administradorDeFundoImobiliarioRepo.SalvarMudancasAsync())
                        return _mapper.Map<AdministradorDeFundoImobiliarioDto>(administradorDeFundoImobiliario);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarAdministradorDeFundoImobiliario(int administradorDeFundoImobiliarioId)
        {
            var administradorDeFundoImobiliario = await _administradorDeFundoImobiliarioRepo.GetAdministradorDeFundoImobiliarioByIdAsync(administradorDeFundoImobiliarioId, false);

            if (administradorDeFundoImobiliario == null)
                throw new Exception("O Administrador de Fundo Imobiliario que tentou deletar não existe.");

            _administradorDeFundoImobiliarioRepo.Deletar(administradorDeFundoImobiliario);

            return await _administradorDeFundoImobiliarioRepo.SalvarMudancasAsync();
        }

        public async Task<AdministradorDeFundoImobiliarioDto> GetAdministradorDeFundoImobiliarioByIdAsync(int administradorDeFundoImobiliarioId, bool includeFundoImobiliario)
        {
            try
            {
                var administradorDeFundoImobiliario = await _administradorDeFundoImobiliarioRepo.GetAdministradorDeFundoImobiliarioByIdAsync(administradorDeFundoImobiliarioId, includeFundoImobiliario);

                if (administradorDeFundoImobiliario == null) return null;

                return _mapper.Map<AdministradorDeFundoImobiliarioDto>(administradorDeFundoImobiliario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AdministradorDeFundoImobiliarioDto[]> GetAllAdministradoresDeFundosImobiliariosAsync(bool includeFundoImobiliario)
        {
            try
            {
                var administradorDeFundoImobiliario = await _administradorDeFundoImobiliarioRepo.GetAllAdministradoresDeFundosImobiliariosAsync(includeFundoImobiliario);

                if (administradorDeFundoImobiliario == null) return null;

                return _mapper.Map<AdministradorDeFundoImobiliarioDto[]>(administradorDeFundoImobiliario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model)
        {
            if (model != null)
            {
                var administradorDeFundoImobiliario = _mapper.Map<AdministradorDeFundoImobiliario>(model);

                administradorDeFundoImobiliario.Inativar();
                _administradorDeFundoImobiliarioRepo.Atualizar(administradorDeFundoImobiliario);
                return await _administradorDeFundoImobiliarioRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarAdministradorDeFundoImobiliario(AdministradorDeFundoImobiliarioDto model)
        {
            if (model != null)
            {
                var administradorDeFundoImobiliario = _mapper.Map<AdministradorDeFundoImobiliario>(model);

                administradorDeFundoImobiliario.Reativar();
                _administradorDeFundoImobiliarioRepo.Atualizar(administradorDeFundoImobiliario);
                return await _administradorDeFundoImobiliarioRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}