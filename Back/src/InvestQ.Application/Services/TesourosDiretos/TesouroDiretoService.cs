using System;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.TesourosDiretos;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Interfaces.TesourosDiretos;
using InvestQ.Data.Interfaces.TesourosDiretos;
using InvestQ.Domain.Entities.TesourosDiretos;
using InvestQ.Data.Interfaces.Ativos;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Application.Services.TesourosDiretos
{
    public class TesouroDiretoService : ITesouroDiretoService
    {
        private readonly ITesouroDiretoRepo _tesouroDiretoRepo;
        private readonly IAtivoRepo _ativoRepo;
        private readonly IMapper _mapper;

        public TesouroDiretoService(ITesouroDiretoRepo tesouroDiretoRepo,
                                    IAtivoRepo ativoRepo,
                                    IMapper mapper)
        {
            _tesouroDiretoRepo = tesouroDiretoRepo;
            _ativoRepo = ativoRepo;
            _mapper = mapper;
        }
        public async Task<TesouroDiretoDto> AdicionarTesouroDireto(TesouroDiretoDto model)
        {
            var tesouroDireto = _mapper.Map<TesouroDireto>(model);
            
            if (await _tesouroDiretoRepo.GetTesouroDiretoByDescricaoAsync(tesouroDireto.Descricao) != null)
                throw new Exception("Já existe um Tesouro Direto com essa descrição.");

            if( await _tesouroDiretoRepo.GetTesouroDiretoByIdAsync(tesouroDireto.Id) == null)
            {
                _tesouroDiretoRepo.Adicionar(tesouroDireto);

                if (await _tesouroDiretoRepo.SalvarMudancasAsync())
                {
                    var ativoDto = new AtivoDto();
                    var bytes = new Byte[16];
                    ativoDto.Id = new Guid(bytes);
                    ativoDto.TesouroDiretoId = tesouroDireto.Id;
                    ativoDto.TipoDeAtivo = TipoDeAtivo.TesouroDireto;

                    var ativo = _mapper.Map<Ativo>(ativoDto);
                    
                    _ativoRepo.Adicionar(ativo);

                    if (await _ativoRepo.SalvarMudancasAsync())
                    {
                        var retorno = await _tesouroDiretoRepo.GetTesouroDiretoByIdAsync(tesouroDireto.Id);

                        return _mapper.Map<TesouroDiretoDto>(retorno);
                    }                   
                    
                }
            }

            return null;
        }

        public async Task<TesouroDiretoDto> AtualizarTesouroDireto(TesouroDiretoDto model)
        {
            try
            {
                var tesouroDireto = await _tesouroDiretoRepo.GetTesouroDiretoByIdAsync(model.Id);

                if (tesouroDireto != null)
                {
                    if (tesouroDireto.Inativo)
                        throw new Exception("Não se pode alterar um Tesouro Direto inativo.");

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

        public async Task<bool> DeletarTesouroDireto(Guid tesouroDiretoId)
        {
            var tesouroDireto = await _tesouroDiretoRepo.GetTesouroDiretoByIdAsync(tesouroDiretoId);

            if (tesouroDireto == null)
                throw new Exception("O Tesouro Direto que tentou deletar não existe.");            

            var ativo = await _ativoRepo.GetAtivoByTesouroDiretoIdAsync(tesouroDiretoId);

            if (ativo == null)
                throw new Exception("O Ativo que tentou deletar não existe.");

            _ativoRepo.Deletar(ativo);

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

        public async Task<TesouroDiretoDto[]> GetTeseourosDiretosByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId)
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

        public async Task<TesouroDiretoDto> GetTesouroDiretoByIdAsync(Guid id)
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