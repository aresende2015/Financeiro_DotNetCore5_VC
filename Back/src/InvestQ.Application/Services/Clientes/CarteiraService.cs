using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Application.Interfaces.Clientes;
using InvestQ.Data.Interfaces.Clientes;
using InvestQ.Domain.Entities.Clientes;

namespace InvestQ.Application.Services.Clientes
{
    public class CarteiraService : ICarteiraService
    {
        private readonly ICarteiraRepo _carteiraRepo;
        private readonly IMapper _mapper;

        public CarteiraService(ICarteiraRepo carteiraRepo,
                               IMapper mapper)
        {
            _carteiraRepo = carteiraRepo;
            _mapper = mapper;
        }
        public async Task<CarteiraDto> AdicionarCarteira(CarteiraDto model)
        {
            var carteira = _mapper.Map<Carteira>(model);

            if( await _carteiraRepo.GetCarteiraByIdAsync(carteira.Id, false, false) == null)
            {
                _carteiraRepo.Adicionar(carteira);

                if (await _carteiraRepo.SalvarMudancasAsync())
                {
                    var retorno = await _carteiraRepo.GetCarteiraByIdAsync(carteira.Id, false, false);

                    return _mapper.Map<CarteiraDto>(retorno);
                }
            }

            return null;
        }

        public async Task<CarteiraDto> AtualizarCarteira(CarteiraDto model)
        {
            try
            {
                var carteira = await _carteiraRepo.GetCarteiraByIdAsync(model.Id, false, false);

                if (carteira != null)
                {
                    if (carteira.Inativo)
                        throw new Exception("Não se pode alterar uma Carteira inativa.");

                    _mapper.Map(model, carteira);

                    _carteiraRepo.Atualizar(carteira);

                    if (await _carteiraRepo.SalvarMudancasAsync())
                        return _mapper.Map<CarteiraDto>(carteira);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarCarteira(Guid carteiraId)
        {
            var carteira = await _carteiraRepo.GetCarteiraByIdAsync(carteiraId, false, false);

            if (carteira == null)
                throw new Exception("A Carteira que tentou deletar não existe.");

            _carteiraRepo.Deletar(carteira);

            return await _carteiraRepo.SalvarMudancasAsync();
        }

        public async Task<CarteiraDto[]> GetAllCarteirasAsync(bool includeCliente, bool includeCorretora)
        {
            try
            {
                var carteiras = await _carteiraRepo.GetAllCarteirasAsync(includeCliente, includeCorretora);

                if (carteiras == null) return null;

                return _mapper.Map<CarteiraDto[]>(carteiras)     ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CarteiraDto[]> GetAllCarteirasByClienteIdAsync(Guid clienteId, bool includeCliente, bool includeCorretora)
        {
            try
            {
                var carteiras = await _carteiraRepo.GetAllCarteirasByClienteId(clienteId, includeCliente, includeCorretora);

                if (carteiras == null) return null;

                return _mapper.Map<CarteiraDto[]>(carteiras);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CarteiraDto[]> GetAllCarteirasByCorretoraIdAsync(Guid corretoraId, bool includeCliente, bool includeCorretora)
        {
            try
            {
                var carteiras = await _carteiraRepo.GetAllCarteirasByCorretoraId(corretoraId, includeCliente, includeCorretora);

                if (carteiras == null) return null;

                return _mapper.Map<CarteiraDto[]>(carteiras);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CarteiraDto> GetCarteiraByIdAsync(Guid id, bool includeCliente, bool includeCorretora)
        {
            try
            {
                var carteira = await _carteiraRepo.GetCarteiraByIdAsync(id, includeCliente, includeCorretora);

                if (carteira == null) return null;

                return _mapper.Map<CarteiraDto>(carteira);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarCarteira(CarteiraDto model)
        {
            if (model != null)
            {
                var carteira = _mapper.Map<Carteira>(model);

                carteira.Inativar();
                _carteiraRepo.Atualizar(carteira);
                return await _carteiraRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarCarteira(CarteiraDto model)
        {
            if (model != null)
            {
                var carteira = _mapper.Map<Carteira>(model);

                carteira.Reativar();
                _carteiraRepo.Atualizar(carteira);
                return await _carteiraRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}