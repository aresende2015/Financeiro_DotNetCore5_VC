using System;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.FundosImobiliarios;
using InvestQ.Application.Interfaces.FundosImobiliarios;
using InvestQ.Data.Interfaces.FundosImobiliarios;
using InvestQ.Domain.Entities.FundosImobiliarios;

namespace InvestQ.Application.Services.FundosImobiliarios
{
    public class FundoImobiliarioService : IFundoImobiliarioService
    {
        private readonly IFundoImobiliarioRepo _fundoImobiliarioRepo;
        private readonly IMapper _mapper;

        public FundoImobiliarioService(IFundoImobiliarioRepo fundoImobiliarioRepo,
                                       IMapper mapper)
        {
            _fundoImobiliarioRepo = fundoImobiliarioRepo;
            _mapper = mapper;
        }
        public async Task<FundoImobiliarioDto> AdicionarFundoImobiliario(FundoImobiliarioDto model)
        {
            var fundoImobiliario = _mapper.Map<FundoImobiliario>(model);

            if (await _fundoImobiliarioRepo.GetFundoImobiliarioByDescricaoAsync(fundoImobiliario.Descricao) != null)
                throw new Exception("Já existe um Fundo Imobiliário com essa descrição.");

            if( await _fundoImobiliarioRepo.GetFundoImobiliarioByIdAsync(fundoImobiliario.Id) == null)
            {
                _fundoImobiliarioRepo.Adicionar(fundoImobiliario);

                if (await _fundoImobiliarioRepo.SalvarMudancasAsync())
                {
                    var retorno = await _fundoImobiliarioRepo.GetFundoImobiliarioByIdAsync(fundoImobiliario.Id);

                    return _mapper.Map<FundoImobiliarioDto>(retorno);
                }
            }

            return null;
        }

        public async Task<FundoImobiliarioDto> AtualizarFundoImobiliario(FundoImobiliarioDto model)
        {
            try
            {
                var fundoImobiliario = await _fundoImobiliarioRepo.GetFundoImobiliarioByIdAsync(model.Id);

                if (fundoImobiliario != null)
                {
                    if (fundoImobiliario.Inativo)
                        throw new Exception("Não se pode alterar um Fundo Imobiliário inativo.");

                    _mapper.Map(model, fundoImobiliario);

                    _fundoImobiliarioRepo.Atualizar(fundoImobiliario);

                    if (await _fundoImobiliarioRepo.SalvarMudancasAsync())
                        return _mapper.Map<FundoImobiliarioDto>(fundoImobiliario);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarFundoImobiliario(Guid fundoImobiliarioId)
        {
            var fundoImobiliario = await _fundoImobiliarioRepo.GetFundoImobiliarioByIdAsync(fundoImobiliarioId);

            if (fundoImobiliario == null)
                throw new Exception("O Fundo Imobiliário que tentou deletar não existe.");

            _fundoImobiliarioRepo.Deletar(fundoImobiliario);

            return await _fundoImobiliarioRepo.SalvarMudancasAsync();
        }

        public async Task<FundoImobiliarioDto[]> GetAllFundosImobiliariosAsync()
        {
            try
            {
                var fundosImobiliarios = await _fundoImobiliarioRepo.GetAllFundosImobiliariosAsync();

                if (fundosImobiliarios == null) return null;

                return _mapper.Map<FundoImobiliarioDto[]>(fundosImobiliarios);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FundoImobiliarioDto> GetFundoImobiliarioByIdAsync(Guid id)
        {
            try
            {
                var fundoImobiliario = await _fundoImobiliarioRepo.GetFundoImobiliarioByIdAsync(id);

                if (fundoImobiliario == null) return null;

                return _mapper.Map<FundoImobiliarioDto>(fundoImobiliario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FundoImobiliarioDto> GetFundoImobiliarioByDescricaoAsync(string descricao)
        {
            try
            {
                var fundoImobiliario = await _fundoImobiliarioRepo.GetFundoImobiliarioByDescricaoAsync(descricao);

                if (fundoImobiliario == null) return null;

                return _mapper.Map<FundoImobiliarioDto>(fundoImobiliario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FundoImobiliarioDto[]> GetFundosImobliariosByAdministradorDeFundoImobiliarioIdAsync(Guid administradorDeFundoImobiliarioId)
        {
            try
            {
                var fundosImobiliarios = await _fundoImobiliarioRepo.GetFundosImobliariosByAdministradorDeFundoImobiliarioIdAsync(administradorDeFundoImobiliarioId);

                if (fundosImobiliarios == null) return null;

                var RetornoDto = _mapper.Map<FundoImobiliarioDto[]>(fundosImobiliarios);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FundoImobiliarioDto[]> GetFundosImobliariosBySegmentoAnbimaIdAsync(Guid segmentoAnbimaId)
        {
            try
            {
                var fundosImobiliarios = await _fundoImobiliarioRepo.GetFundosImobliariosBySegmentoAnbimaIdAsync(segmentoAnbimaId);

                if (fundosImobiliarios == null) return null;

                var RetornoDto = _mapper.Map<FundoImobiliarioDto[]>(fundosImobiliarios);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FundoImobiliarioDto[]> GetFundosImobliariosByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId)
        {
            try
            {
                var fundosImobiliarios = await _fundoImobiliarioRepo.GetFundosImobliariosByTipoDeInvestimentoIdAsync(tipoDeInvestimentoId);

                if (fundosImobiliarios == null) return null;

                var RetornoDto = _mapper.Map<FundoImobiliarioDto[]>(fundosImobiliarios);

                return RetornoDto;     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InativarFundoImobiliario(FundoImobiliarioDto model)
        {
            if (model != null)
            {
                var fundoImobiliario = _mapper.Map<FundoImobiliario>(model);

                fundoImobiliario.Inativar();
                _fundoImobiliarioRepo.Atualizar(fundoImobiliario);
                return await _fundoImobiliarioRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> ReativarFundoImobiliario(FundoImobiliarioDto model)
        {
            if (model != null)
            {
                var fundoImobiliario = _mapper.Map<FundoImobiliario>(model);

                fundoImobiliario.Reativar();
                _fundoImobiliarioRepo.Atualizar(fundoImobiliario);
                return await _fundoImobiliarioRepo.SalvarMudancasAsync();
            }

            return false;
        }
    }
}