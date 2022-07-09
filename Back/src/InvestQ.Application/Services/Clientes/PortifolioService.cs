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
    public class PortifolioService : IPortifolioService
    {
        private readonly IPortifolioRepo _portifolioRepo;
        private readonly IMapper _mapper;

        public PortifolioService(IPortifolioRepo portifolioRepo,
                                 IMapper mapper)
        {
            _portifolioRepo = portifolioRepo;
            _mapper = mapper;
        }

        public async Task<PortifolioDto> AdicionarPortifolio(PortifolioDto model)
        {
            var portifolio = _mapper.Map<Portifolio>(model);

            if( await _portifolioRepo.GetPortifolioByIdAsync(portifolio.Id) == null)
            {
                _portifolioRepo.Adicionar(portifolio);

                if (await _portifolioRepo.SalvarMudancasAsync())
                {
                    var retorno = await _portifolioRepo.GetPortifolioByIdAsync(portifolio.Id);

                    return _mapper.Map<PortifolioDto>(retorno);
                }
            }

            return null;
        }

        public async Task<PortifolioDto> AtualizarPortifolio(PortifolioDto model)
        {
            try
            {
                var portifolio = await _portifolioRepo.GetPortifolioByIdAsync(model.Id);

                if (portifolio != null)
                {
                    if (portifolio.Inativo)
                        throw new Exception("Não se pode alterar um Portifolio inativo.");

                    _mapper.Map(model, portifolio);

                    _portifolioRepo.Atualizar(portifolio);

                    if (await _portifolioRepo.SalvarMudancasAsync())
                        return _mapper.Map<PortifolioDto>(portifolio);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarPortifolio(Guid portifolioId)
        {
            var portifolio = await _portifolioRepo.GetPortifolioByIdAsync(portifolioId);

            if (portifolio == null)
                throw new Exception("O Portifolio que tentou deletar não existe.");

            _portifolioRepo.Deletar(portifolio);

            return await _portifolioRepo.SalvarMudancasAsync();
        }

        public async Task<PortifolioDto[]> GetAllPortifoliosByCarteiraIdAsync(Guid carteiraId)
        {
            try
            {
                var portifolios = await _portifolioRepo.GetAllPortifoliosByCarteiraIdAsync(carteiraId);

                if (portifolios == null) return null;

                return _mapper.Map<PortifolioDto[]>(portifolios);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PortifolioDto> GetPortifolioByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId)
        {
            try
            {
                var portifolio = await _portifolioRepo.GetPortifolioByCarteiraIdAtivoIdAsync(carteiraId, ativoId);

                if (portifolio == null) return null;

                return _mapper.Map<PortifolioDto>(portifolio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}