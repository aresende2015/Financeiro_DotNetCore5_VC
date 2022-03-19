using System;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.TesourosDiretos;
using InvestQ.Application.Interfaces.TesourosDiretos;
using InvestQ.Data.Interfaces.TesourosDiretos;
using InvestQ.Domain.Entities.TesourosDiretos;

namespace InvestQ.Application.Services.TesourosDiretos
{
    public class TesouroDiretoService : ITesouroDiretoService
    {
        private readonly ITesouroDiretoRepo _tesouroDiretoRepo;
        private readonly IMapper _mapper;

        public TesouroDiretoService(ITesouroDiretoRepo tesouroDiretoRepo,
                                    IMapper mapper)
        {
            _tesouroDiretoRepo = tesouroDiretoRepo;
            _mapper = mapper;
        }
        public async Task<TesouroDiretoDto> AdicionarTesouroDireto(TesouroDiretoDto model)
        {
            if (model.Inativo)
                throw new Exception("Não é possível incluir um Tesouro Direto inativo.");
            
            var tesouroDireto = _mapper.Map<TesouroDireto>(model);

            if (await _tesouroDiretoRepo.GetTesouroDiretoByDescricaoAsync(tesouroDireto.Descricao) != null)
                throw new Exception("Já existe um Tesouro Direto com essa descrição.");

            if( await _tesouroDiretoRepo.GetTesouroDiretoByIdAsync(tesouroDireto.Id) == null)
            {
                _tesouroDiretoRepo.Adicionar(tesouroDireto);

                if (await _tesouroDiretoRepo.SalvarMudancasAsync())
                {
                    var retorno = await _tesouroDiretoRepo.GetTesouroDiretoByIdAsync(tesouroDireto.Id);

                    return _mapper.Map<TesouroDiretoDto>(retorno);
                }
            }

            return null;
        }

        public async Task<TesouroDiretoDto> AtualizarTesouroDireto(TesouroDiretoDto model)
        {
            try
            {
                if (model.Inativo)
                    throw new Exception("Não é possível atualizar um Tesouro Direto já inativo.");

                var tesouroDireto = await _tesouroDiretoRepo.GetTesouroDiretoByIdAsync(model.Id);

                if (tesouroDireto != null)
                {
                    if (tesouroDireto.Inativo)
                        throw new Exception("Não se pode alterar um Tesouro Direto inativo.");

                    model.Inativo = tesouroDireto.Inativo;
                    model.DataDeCriacao = tesouroDireto.DataDeCriacao;

                    _mapper.Map(model, tesouroDireto);

                    _tesouroDiretoRepo.Atualizar(tesouroDireto);

                    if (await _tesouroDiretoRepo.SalvarMudancasAsync())
                        return _mapper.Map<TesouroDiretoDto>(tesouroDireto);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarTesouroDireto(int tesouroDiretoId)
        {
            var tesouroDireto = await _tesouroDiretoRepo.GetTesouroDiretoByIdAsync(tesouroDiretoId);

            if (tesouroDireto == null)
                throw new Exception("O Tesouro Direto que tentou deletar não existe.");

            _tesouroDiretoRepo.Deletar(tesouroDireto);

            return await _tesouroDiretoRepo.SalvarMudancasAsync();
        }

        public async Task<TesouroDiretoDto[]> GetAllTeseourosDiretosAsync()
        {
            try
            {
                var tesourosDiretos = await _tesouroDiretoRepo.GetAllTeseourosDiretosAsync();

                if (tesourosDiretos == null) return null;

                return _mapper.Map<TesouroDiretoDto[]>(tesourosDiretos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TesouroDiretoDto[]> GetTeseourosDiretosByTipoDeInvestimentoIdAsync(int tipoDeInvestimentoId)
        {
            try
            {
                var tesourosDiretos = await _tesouroDiretoRepo.GetTeseourosDiretosByTipoDeInvestimentoIdAsync(tipoDeInvestimentoId);

                if (tesourosDiretos == null) return null;

                var RetornoDto = _mapper.Map<TesouroDiretoDto[]>(tesourosDiretos);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TesouroDiretoDto> GetTesouroDiretoByDescricaoAsync(string descricao)
        {
            try
            {
                var tesouroDireto = await _tesouroDiretoRepo.GetTesouroDiretoByDescricaoAsync(descricao);

                if (tesouroDireto == null) return null;

                return _mapper.Map<TesouroDiretoDto>(tesouroDireto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TesouroDiretoDto> GetTesouroDiretoByIdAsync(int id)
        {
            try
            {
                var tesouroDireto = await _tesouroDiretoRepo.GetTesouroDiretoByIdAsync(id);

                if (tesouroDireto == null) return null;

                return _mapper.Map<TesouroDiretoDto>(tesouroDireto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarTesouroDireto(TesouroDiretoDto model)
        {
            if (model != null)
            {
                var tesouroDiretoDto = _mapper.Map<TesouroDireto>(model);

                tesouroDiretoDto.Inativar();
                _tesouroDiretoRepo.Atualizar(tesouroDiretoDto);
                return await _tesouroDiretoRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarTesouroDireto(TesouroDiretoDto model)
        {
            if (model != null)
            {
                var tesouroDiretoDto = _mapper.Map<TesouroDireto>(model);

                tesouroDiretoDto.Reativar();
                _tesouroDiretoRepo.Atualizar(tesouroDiretoDto);
                return await _tesouroDiretoRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}