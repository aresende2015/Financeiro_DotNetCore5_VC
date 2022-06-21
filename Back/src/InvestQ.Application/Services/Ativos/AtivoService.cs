using System;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Dtos.Enum;
using InvestQ.Application.Interfaces.Ativos;
using InvestQ.Data.Interfaces.Ativos;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Application.Services.Ativos
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
                var ativo = await _ativoRepo.GetAtivoByIdAsync(model.Id);

                if (ativo != null)
                {
                    if (ativo.Inativo)
                        throw new Exception("Não se pode alterar um Ativo inativo.");

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

        public async Task<bool> DeletarAtivo(Guid ativoId)
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

        public async Task<AtivoDto> GetAtivoByAcaoIdAsync(Guid acaoId)
        {
            try
            {
                var ativo = await _ativoRepo.GetAtivoByAcaoIdAsync(acaoId);

                if (ativo == null) return null;

                return _mapper.Map<AtivoDto>(ativo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AtivoDto> GetAtivoByFundoImobiliarioIdAsync(Guid fundoImobiliarioId)
        {
            try
            {
                var ativo = await _ativoRepo.GetAtivoByFundoImobiliarioIdAsync(fundoImobiliarioId);

                if (ativo == null) return null;

                return _mapper.Map<AtivoDto>(ativo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AtivoDto> GetAtivoByIdAsync(Guid id)
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

        public async Task<AtivoDto> GetAtivoByIdsAsync(Guid id, TipoDeAtivoDto tipoDeAtivoDto)
        {
            try
            {
                var tipoDeAtivo = _mapper.Map<TipoDeAtivo>(tipoDeAtivoDto);
                
                var ativo = await _ativoRepo.GetAtivoByIdsAsync(id, tipoDeAtivo);

                if (ativo == null) return null;

                return _mapper.Map<AtivoDto>(ativo);  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AtivoDto> GetAtivoByTesouroDiretoIdAsync(Guid tesouroDiretoId)
        {
            try
            {
                var ativo = await _ativoRepo.GetAtivoByTesouroDiretoIdAsync(tesouroDiretoId);

                if (ativo == null) return null;

                return _mapper.Map<AtivoDto>(ativo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AtivoDto> GetAtivoByTipoDeAtivoDescricaoAsync(TipoDeAtivoDto tipoDeAtivoDto, string descricao)
        {
            try
            {
                var tipoDeAtivo = _mapper.Map<TipoDeAtivo>(tipoDeAtivoDto);

                var ativo = await _ativoRepo.GetAtivoByTipoDeAtivoDescricaoAsync(tipoDeAtivo, descricao);

                if (ativo == null) return null;

                return _mapper.Map<AtivoDto>(ativo);
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