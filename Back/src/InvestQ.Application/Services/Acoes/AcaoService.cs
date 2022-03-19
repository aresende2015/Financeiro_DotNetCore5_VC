using System;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Acoes;
using InvestQ.Application.Interfaces.Acoes;
using InvestQ.Data.Interfaces.Acoes;
using InvestQ.Domain.Entities.Acoes;

namespace InvestQ.Application.Services.Acoes
{
    public class AcaoService : IAcaoService
    {
        private readonly IAcaoRepo _acaoRepo;
        private readonly IMapper _mapper;

        public AcaoService(IAcaoRepo acaoRepo,
                                     IMapper mapper)
        {
            _acaoRepo = acaoRepo;
            _mapper = mapper;
        }
        public async Task<AcaoDto> AdicionarAcao(AcaoDto model)
        {   
            var acao = _mapper.Map<Acao>(model);

            if (await _acaoRepo.GetAcaoByCodigoAsync(acao.Codigo) != null)
                throw new Exception("Já existe uma Ação com esse código.");

            if( await _acaoRepo.GetAcaoByIdAsync(acao.Id) == null)
            {
                _acaoRepo.Adicionar(acao);

                if (await _acaoRepo.SalvarMudancasAsync())
                {
                    var retorno = await _acaoRepo.GetAcaoByIdAsync(acao.Id);

                    return _mapper.Map<AcaoDto>(retorno);
                }
            }

            return null;
        }

        public async Task<AcaoDto> AtualizarAcao(AcaoDto model)
        {
            try
            {
                var acao = await _acaoRepo.GetAcaoByIdAsync(model.Id);

                if (acao != null)
                {
                    if (acao.Inativo)
                        throw new Exception("Não se pode alterar uma Ação inativa.");

                    _mapper.Map(model, acao);

                    _acaoRepo.Atualizar(acao);

                    if (await _acaoRepo.SalvarMudancasAsync())
                        return _mapper.Map<AcaoDto>(acao);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarAcao(Guid acaoId)
        {
            var acao = await _acaoRepo.GetAcaoByIdAsync(acaoId);

            if (acao == null)
                throw new Exception("A Ação que tentou deletar não existe.");

            _acaoRepo.Deletar(acao);

            return await _acaoRepo.SalvarMudancasAsync();
        }

        public async Task<AcaoDto> GetAcaoByCodigoAsync(string codigo)
        {
            try
            {
                var acao = await _acaoRepo.GetAcaoByCodigoAsync(codigo);

                if (acao == null) return null;

                return _mapper.Map<AcaoDto>(acao);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AcaoDto> GetAcaoByIdAsync(Guid id)
        {
            try
            {
                var acao = await _acaoRepo.GetAcaoByIdAsync(id);

                if (acao == null) return null;

                return _mapper.Map<AcaoDto>(acao);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AcaoDto[]> GetAcoesBySegmentoIdAsync(Guid segmentoId)
        {
            try
            {
                var acoes = await _acaoRepo.GetAcoesBySegmentoIdAsync(segmentoId);

                if (acoes == null) return null;

                var RetornoDto = _mapper.Map<AcaoDto[]>(acoes);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AcaoDto[]> GetAcoesByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId)
        {
            try
            {
                var acoes = await _acaoRepo.GetAcoesByTipoDeInvestimentoIdAsync(tipoDeInvestimentoId);

                if (acoes == null) return null;

                var RetornoDto = _mapper.Map<AcaoDto[]>(acoes);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AcaoDto[]> GetAllAcoesAsync()
        {
            try
            {
                var acoes = await _acaoRepo.GetAllAcoesAsync();

                if (acoes == null) return null;

                return _mapper.Map<AcaoDto[]>(acoes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarAcao(AcaoDto model)
        {
            if (model != null)
            {
                var acao = _mapper.Map<Acao>(model);

                acao.Inativar();
                _acaoRepo.Atualizar(acao);
                return await _acaoRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarAcao(AcaoDto model)
        {
            if (model != null)
            {
                var acao = _mapper.Map<Acao>(model);

                acao.Reativar();
                _acaoRepo.Atualizar(acao);
                return await _acaoRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}