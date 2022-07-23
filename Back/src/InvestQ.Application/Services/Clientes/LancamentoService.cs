using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Application.Interfaces.Clientes;
using InvestQ.Data.Interfaces.Clientes;
using InvestQ.Data.Paginacao;
using InvestQ.Domain.Entities.Clientes;
using InvestQ.Domain.Enum;

namespace InvestQ.Application.Services.Clientes
{
    public class LancamentoService : ILancamentoService
    {
        private readonly ILancamentoRepo _lancamentoRepo;
        private readonly IPortifolioRepo _portifolioRepo;
        private readonly IMapper _mapper;

        public LancamentoService(ILancamentoRepo lancamentoRepo,
                                 IPortifolioRepo portifolioRepo,
                                 IMapper mapper)
        {
            _lancamentoRepo = lancamentoRepo;
            _portifolioRepo = portifolioRepo;
            _mapper = mapper;
        }
        public async Task<LancamentoDto> AdicionarLancamento(LancamentoDto model)
        {
            var _tipoDeAtivo = model.TipoDeAtivo;

            var lancamento = _mapper.Map<Lancamento>(model);

            if( await _lancamentoRepo.GetLancamentoByIdAsync(lancamento.Id, false, false) == null)
            {
                if (_tipoDeAtivo == TipoDeAtivo.Acao)
                {
                    if ( lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Compra || 
                         lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Venda)
                    {
                        var lancamentosDaCarteiraDoAtivoCompraVenda =  
                                await _lancamentoRepo
                                        .GetAllLancamentosByCarteiraIdAtivoIdCompraVendaAsync(lancamento.CarteiraId
                                                                                            , lancamento.AtivoId
                                                                                            , lancamento.Id);

                        if (lancamentosDaCarteiraDoAtivoCompraVenda.Count() > 0) {
                            ContabilizarOperacaoDaytrade(lancamento
                                                    , lancamentosDaCarteiraDoAtivoCompraVenda
                                                    , TipoDeAcaoDoUsuario.Insert);
                        }

                        await AtualizarPortifolioDaCarteiraDoAtivo(lancamento
                                                                , lancamentosDaCarteiraDoAtivoCompraVenda
                                                                , TipoDeAcaoDoUsuario.Insert);
                        
                        _lancamentoRepo.AtualizarVarias(lancamentosDaCarteiraDoAtivoCompraVenda);
                    }
                }

                _lancamentoRepo.Adicionar(lancamento);

                if (await _lancamentoRepo.SalvarMudancasAsync())
                {
                    var retorno = await _lancamentoRepo.GetLancamentoByIdAsync(lancamento.Id, true, true);

                    return _mapper.Map<LancamentoDto>(retorno);
                }

            }

            return null;
        }

        public async Task<LancamentoDto> AtualizarLancamento(LancamentoDto model)
        {
            try
            {
                var lancamento = await _lancamentoRepo.GetLancamentoByIdAsync(model.Id, false, false);

                if (lancamento != null)
                {
                    if (lancamento.Inativo)
                        throw new Exception("Não se pode alterar um Lançamento inativo.");

                    _mapper.Map(model, lancamento);

                    var lancamentosDaCarteiraDoAtivoCompraVenda = await _lancamentoRepo
                                                            .GetAllLancamentosByCarteiraIdAtivoIdCompraVendaAsync(lancamento.CarteiraId
                                                                                                                , lancamento.AtivoId
                                                                                                                , lancamento.Id);

                    ContabilizarOperacaoDaytrade(lancamento
                                                , lancamentosDaCarteiraDoAtivoCompraVenda
                                                , TipoDeAcaoDoUsuario.Update);

                    await AtualizarPortifolioDaCarteiraDoAtivo(lancamento
                                                             , lancamentosDaCarteiraDoAtivoCompraVenda
                                                             , TipoDeAcaoDoUsuario.Update);

                    _lancamentoRepo.AtualizarVarias(lancamentosDaCarteiraDoAtivoCompraVenda); 

                    _lancamentoRepo.Atualizar(lancamento);

                    if (await _lancamentoRepo.SalvarMudancasAsync())
                        return _mapper.Map<LancamentoDto>(lancamento);
                }

                return null;
            }
            catch (Exception ex)
            {                
               throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeletarLancamento(Guid lancamentoId)
        {
            var lancamento = await _lancamentoRepo.GetLancamentoByIdAsync(lancamentoId, false, false);

            if (lancamento == null)
                throw new Exception("A Lancamento que tentou deletar não existe.");

            var lancamentosDaCarteiraDoAtivoCompraVenda =  
                                await _lancamentoRepo
                                        .GetAllLancamentosByCarteiraIdAtivoIdCompraVendaAsync(lancamento.CarteiraId
                                                                                            , lancamento.AtivoId
                                                                                            , lancamento.Id);
            
            if (lancamento.QuantidadeDayTrade > 0)
            {
                ContabilizarOperacaoDaytrade(lancamento
                                            , lancamentosDaCarteiraDoAtivoCompraVenda
                                            , TipoDeAcaoDoUsuario.Delete);
            }

            await AtualizarPortifolioDaCarteiraDoAtivo(lancamento
                                                     , lancamentosDaCarteiraDoAtivoCompraVenda
                                                     , TipoDeAcaoDoUsuario.Delete);

            _lancamentoRepo.AtualizarVarias(lancamentosDaCarteiraDoAtivoCompraVenda);

            _lancamentoRepo.Deletar(lancamento);

            return await _lancamentoRepo.SalvarMudancasAsync();
        }

        public async Task<PageList<LancamentoDto>> GetAllLancamentosByCarteiraIdAsync(Guid carteiraId, PageParams pageParams)
        {
            try
            {
                var lancamentos = await _lancamentoRepo.GetAllLancamentosByCarteiraIdAsync(carteiraId, pageParams);

                if (lancamentos == null) return null;

                var RetornoDto = _mapper.Map<PageList<LancamentoDto>>(lancamentos);

                RetornoDto.CurrentPage = lancamentos.CurrentPage;
                RetornoDto.TotalPages = lancamentos.TotalPages;
                RetornoDto.PageSize = lancamentos.PageSize;
                RetornoDto.TotalCount = lancamentos.TotalCount;

                return RetornoDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LancamentoDto[]> GetAllLancamentosByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId, PageParams pageParams, bool includeCarteira, bool includeAtivo)
        {
            try
            {
                var lancamentos = await _lancamentoRepo.GetAllLancamentosByCarteiraIdAtivoIdAsync(carteiraId, ativoId, pageParams, includeCarteira, includeAtivo);

                if (lancamentos == null) return null;

                return _mapper.Map<LancamentoDto[]>(lancamentos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LancamentoDto> GetLancamentoByIdAsync(Guid id, bool includeCarteira, bool includeAtivo)
        {
            try
            {
                var lancamento = await _lancamentoRepo.GetLancamentoByIdAsync(id, includeCarteira, includeAtivo);

                if (lancamento == null) return null;

                return _mapper.Map<LancamentoDto>(lancamento);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool GetPossuiLancamentosByCarteiraId(Guid carteiraId)
        {
            return _lancamentoRepo.GetPossuiLancamentoByCarteiraId(carteiraId);
        }

        private void ContabilizarOperacaoDaytrade(Lancamento lancamentoAtual
                                                , Lancamento[] lancamentosDaCarteiraDoAtivoCompraVenda
                                                , TipoDeAcaoDoUsuario tipoDeAcaoDoUsuario)
        {
            if (tipoDeAcaoDoUsuario == TipoDeAcaoDoUsuario.Delete || tipoDeAcaoDoUsuario == TipoDeAcaoDoUsuario.Update) 
            {
                ExcluirDayTradeDaCarteiraDoAtivo(lancamentoAtual, lancamentosDaCarteiraDoAtivoCompraVenda);
            }

            if (tipoDeAcaoDoUsuario == TipoDeAcaoDoUsuario.Insert  || tipoDeAcaoDoUsuario == TipoDeAcaoDoUsuario.Update)
            {
                IncluirDayTradeDaCarteiraDoAtivo(lancamentoAtual, lancamentosDaCarteiraDoAtivoCompraVenda);
            }            
        }

        private void ExcluirDayTradeDaCarteiraDoAtivo(Lancamento excluirLancamento
                                                    , Lancamento[] lancamentosDaCarteiraDoAtivoCompraVenda)
        {
            var _quatidadeDayTrade = excluirLancamento.QuantidadeDayTrade;

            foreach (Lancamento _lancamentoDodia in lancamentosDaCarteiraDoAtivoCompraVenda)
            {                            
                if (_lancamentoDodia.TipoDeMovimentacao != excluirLancamento.TipoDeMovimentacao &&
                    _lancamentoDodia.DataDaOperacao.Date == excluirLancamento.DataDaOperacao.Date &&
                    _lancamentoDodia.QuantidadeDayTrade > 0 &&
                    _quatidadeDayTrade > 0)
                {
                    if (_lancamentoDodia.QuantidadeDayTrade >= _quatidadeDayTrade)
                    {
                        _lancamentoDodia.QuantidadeDayTrade = excluirLancamento.QuantidadeDayTrade - _quatidadeDayTrade;
                        _quatidadeDayTrade = 0;
                    } else {
                        _quatidadeDayTrade = _quatidadeDayTrade - _lancamentoDodia.QuantidadeDayTrade;
                        _lancamentoDodia.QuantidadeDayTrade = 0;
                    }
                    excluirLancamento.Contabilizado = false;
                }
            }
        }

        private void IncluirDayTradeDaCarteiraDoAtivo(Lancamento incluirLancamento
                                                    , Lancamento[] lancamentosDaCarteiraDoAtivoCompraVenda)
        {
            var _quantidadeDaOperacao = incluirLancamento.Quantidade;

            foreach (Lancamento _lancamentoDodia in lancamentosDaCarteiraDoAtivoCompraVenda)
            {                            
                if (_lancamentoDodia.TipoDeMovimentacao != incluirLancamento.TipoDeMovimentacao &&
                    _lancamentoDodia.DataDaOperacao.Date == incluirLancamento.DataDaOperacao.Date)
                {
                    var _quantidadeDayTrade = _lancamentoDodia.Quantidade - _lancamentoDodia.QuantidadeDayTrade;

                    if (_quantidadeDayTrade > 0)
                    {
                        if (_quantidadeDaOperacao >= _quantidadeDayTrade)
                        {
                            _quantidadeDaOperacao = _quantidadeDaOperacao - _quantidadeDayTrade;
                            _lancamentoDodia.QuantidadeDayTrade = _lancamentoDodia.QuantidadeDayTrade + _quantidadeDayTrade;
                        } else 
                        {
                            _lancamentoDodia.QuantidadeDayTrade = _lancamentoDodia.QuantidadeDayTrade + _quantidadeDaOperacao;
                            _quantidadeDaOperacao = 0;

                        }
                        _lancamentoDodia.Contabilizado = false;
                    }
                }
            }

            incluirLancamento.Contabilizado = false;

            if (_quantidadeDaOperacao != incluirLancamento.Quantidade)
            {                
                incluirLancamento.QuantidadeDayTrade = incluirLancamento.Quantidade - _quantidadeDaOperacao;
            }
        }

        private async Task AtualizarPortifolioDaCarteiraDoAtivo(Lancamento lancamento
                                                              , Lancamento[] lancamentosDaCarteiraDoAtivo
                                                              , TipoDeAcaoDoUsuario tipoDeAcaoDoUsuario)
        {
            var portifolio = await _portifolioRepo.GetPortifolioByCarteiraIdAtivoIdAsync(lancamento.CarteiraId, lancamento.AtivoId);

            if (portifolio == null) {
                if (lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Compra) {
                    portifolio = new Portifolio();
                    portifolio.AtivoId = lancamento.AtivoId;
                    portifolio.CarteiraId = lancamento.CarteiraId;
                    portifolio.Quantidade = lancamento.Quantidade;
                    portifolio.PrecoMedio = lancamento.ValorDaOperacao;

                    _portifolioRepo.Adicionar(portifolio);
                    lancamento.Contabilizado = true; 
                } else {
                    throw new Exception("Não pode ocorrer uma venda sem ter o ativo.");
                }
            } else {
                if (lancamentosDaCarteiraDoAtivo.Count() > 0) {
                    DateTime _maiorDataDeOperacao = _lancamentoRepo
                                                        .GetDataLancamentoByCarteiraIdAtivoIdAsync(lancamento.CarteiraId, lancamento.AtivoId);

                    if (tipoDeAcaoDoUsuario == TipoDeAcaoDoUsuario.Insert && lancamento.DataDaOperacao.Date > _maiorDataDeOperacao.Date)
                    {
                        if (lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Compra)
                        {
                            IncluirLancamentoDeCompraNoPortifolio(portifolio, lancamento);
                        } else {
                            if (portifolio.Quantidade >= lancamento.Quantidade){
                                IncluirLancamentoDeVendaNoPortifolio(portifolio, lancamento);
                            } else {
                                throw new Exception("Não pode ocorrer uma venda sem ter a quantidade do ativo.");
                            }
                        }
                        lancamento.Contabilizado = true; 
                    } else {
                        RecalcularPortifolioDaCarteiraDoAtivo(portifolio
                                                            , lancamento
                                                            , lancamentosDaCarteiraDoAtivo
                                                            , tipoDeAcaoDoUsuario);
                    }                    
                } else {
                    if (lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Compra)
                    {
                        IncluirLancamentoDeCompraNoPortifolio(portifolio, lancamento);
                    } else {
                        if (portifolio.Quantidade >= lancamento.Quantidade){
                            IncluirLancamentoDeVendaNoPortifolio(portifolio, lancamento);
                        } else {
                            throw new Exception("Não pode ocorrer uma venda sem ter a quantidade do ativo.");
                        }
                    }
                    lancamento.Contabilizado = true;
                }

                _portifolioRepo.Atualizar(portifolio);
            }
        }

        private void RecalcularPortifolioDaCarteiraDoAtivo(Portifolio portifolio
                                                         , Lancamento lancamentoAtual
                                                         , Lancamento[] lancamentosDaCarteiraDoAtivo
                                                         , TipoDeAcaoDoUsuario tipoDeAcaoDoUsuario) {

            portifolio.PrecoMedio = 0;
            portifolio.Quantidade = 0;

            foreach (Lancamento _lancamento in lancamentosDaCarteiraDoAtivo)
            {
                if (tipoDeAcaoDoUsuario == TipoDeAcaoDoUsuario.Insert || tipoDeAcaoDoUsuario == TipoDeAcaoDoUsuario.Update)
                {
                    if ((lancamentoAtual.DataDaOperacao.Date < _lancamento.DataDaOperacao.Date) && 
                     !lancamentoAtual.Contabilizado)
                    {
                        lancamentoAtual.Contabilizado = true;

                        if (lancamentoAtual.TipoDeMovimentacao == TipoDeMovimentacao.Compra)
                        {
                            IncluirLancamentoDeCompraNoPortifolio(portifolio, lancamentoAtual);
                        } else {
                            if (portifolio.Quantidade >= (lancamentoAtual.Quantidade - lancamentoAtual.QuantidadeDayTrade)){
                                IncluirLancamentoDeVendaNoPortifolio(portifolio, lancamentoAtual);
                            } else {
                                throw new Exception("Não pode ocorrer uma venda sem ter a quantidade do ativo.");
                            }
                        }
                    }
                }

                if (_lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Compra)
                {
                    IncluirLancamentoDeCompraNoPortifolio(portifolio, _lancamento);
                    _lancamento.Contabilizado = true;
                } else if (_lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Venda) {
                    if (portifolio.Quantidade >= (_lancamento.Quantidade - _lancamento.QuantidadeDayTrade)){
                        IncluirLancamentoDeVendaNoPortifolio(portifolio, _lancamento);
                        _lancamento.Contabilizado = true;
                    } else {
                        throw new Exception("Não pode ocorrer uma venda sem ter a quantidade do ativo.");
                    }
                } 

                if (portifolio.Quantidade < 0)
                    throw new Exception("Não pode ocorrer uma venda sem ter a quantidade do ativo.");
            
            }

            if (!lancamentoAtual.Contabilizado && tipoDeAcaoDoUsuario == TipoDeAcaoDoUsuario.Update)
            {
                if (lancamentoAtual.TipoDeMovimentacao == TipoDeMovimentacao.Compra)
                {
                    IncluirLancamentoDeCompraNoPortifolio(portifolio, lancamentoAtual);
                } else {
                    if (portifolio.Quantidade >= (lancamentoAtual.Quantidade - lancamentoAtual.QuantidadeDayTrade)){
                        IncluirLancamentoDeVendaNoPortifolio(portifolio, lancamentoAtual);
                    } else {
                        throw new Exception("Não pode ocorrer uma venda sem ter a quantidade do ativo.");
                    }
                }

                if (portifolio.Quantidade < 0)
                    throw new Exception("Não pode ocorrer uma venda sem ter a quantidade do ativo.");
            }
        }
    
        private void IncluirLancamentoDeCompraNoPortifolio(Portifolio portifolio
                                                       , Lancamento lancamento)
        {
            var _quantidadeTotal = portifolio.Quantidade + (lancamento.Quantidade - lancamento.QuantidadeDayTrade);
                    
            decimal _precoMedio = 0;

            if (_quantidadeTotal > 0)
                _precoMedio = ( (portifolio.Quantidade * portifolio.PrecoMedio) + 
                                    ((lancamento.Quantidade - lancamento.QuantidadeDayTrade) * lancamento.ValorDaOperacao)) / 
                                    _quantidadeTotal;
            
            portifolio.Quantidade = _quantidadeTotal;
            portifolio.PrecoMedio = _precoMedio;
        }

        private void IncluirLancamentoDeVendaNoPortifolio(Portifolio portifolio
                                                        , Lancamento lancamento) 
        {
            var _quantidadeTotal = portifolio.Quantidade - (lancamento.Quantidade - lancamento.QuantidadeDayTrade);
                        
            if (_quantidadeTotal == 0)
                    portifolio.PrecoMedio = 0;

            portifolio.Quantidade = _quantidadeTotal;
        }
    }
}