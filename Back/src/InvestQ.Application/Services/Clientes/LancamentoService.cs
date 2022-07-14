using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Application.Interfaces.Clientes;
using InvestQ.Data.Interfaces.Clientes;
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

            if( await _lancamentoRepo.GetLancamentoByIdAsync(lancamento.Id) == null)
            {
                _lancamentoRepo.Adicionar(lancamento);

                if (_tipoDeAtivo == TipoDeAtivo.Acao)
                {
                    if ( lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Compra || 
                         lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Venda)
                    {
                        await AdicionarPortifolioAcao(lancamento);
                    }
                }

                if (await _lancamentoRepo.SalvarMudancasAsync())
                {
                    var retorno = await _lancamentoRepo.GetLancamentoByIdAsync(lancamento.Id);

                    return _mapper.Map<LancamentoDto>(retorno);
                }
            }

            return null;
        }

        public async Task<LancamentoDto> AtualizarLancamento(LancamentoDto model)
        {
            try
            {
                var lancamento = await _lancamentoRepo.GetLancamentoByIdAsync(model.Id);

                if (lancamento != null)
                {
                    if (lancamento.Inativo)
                        throw new Exception("Não se pode alterar um Lançamento inativo.");

                    _mapper.Map(model, lancamento);

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
            var lancamento = await _lancamentoRepo.GetLancamentoByIdAsync(lancamentoId);

            if (lancamento == null)
                throw new Exception("A Lancamento que tentou deletar não existe.");

            _lancamentoRepo.Deletar(lancamento);

            return await _lancamentoRepo.SalvarMudancasAsync();
        }

        public async Task<LancamentoDto[]> GetAllLancamentosByCarteiraIdAsync(Guid carteiraId)
        {
            try
            {
                var lancamentos = await _lancamentoRepo.GetAllLancamentosByCarteiraIdAsync(carteiraId);

                if (lancamentos == null) return null;

                return _mapper.Map<LancamentoDto[]>(lancamentos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LancamentoDto[]> GetAllLancamentosByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId, bool includeCarteira, bool includeAtivo)
        {
            try
            {
                var lancamentos = await _lancamentoRepo.GetAllLancamentosByCarteiraIdAtivoIdAsync(carteiraId, ativoId, includeCarteira, includeAtivo);

                if (lancamentos == null) return null;

                return _mapper.Map<LancamentoDto[]>(lancamentos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LancamentoDto> GetLancamentoByIdAsync(Guid id)
        {
            try
            {
                var lancamento = await _lancamentoRepo.GetLancamentoByIdAsync(id);

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

        private async Task AdicionarPortifolioAcao(Lancamento lancamento)
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
                    //_lancamentoRepo.Atualizar(lancamento);
                } else {
                    throw new Exception("Não pode ocorrer uma venda sem ter o ativo.");
                }
            }
            else {
                var _maiorDataDeOperacao = _lancamentoRepo.GetDataLancamentoByCarteiraIdAtivoIdAsync
                                                    (lancamento.CarteiraId, lancamento.AtivoId);

                if (lancamento.DataDaOperacao.Date >= _maiorDataDeOperacao.Date)
                {
                    if (lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Compra)
                    {
                        var _quantidadeTotal = portifolio.Quantidade + lancamento.Quantidade;
                        var _precoMedio = ( (portifolio.Quantidade * portifolio.PrecoMedio) + 
                                            (lancamento.Quantidade * lancamento.ValorDaOperacao)) / 
                                            _quantidadeTotal;
                        
                        portifolio.Quantidade = _quantidadeTotal;
                        portifolio.PrecoMedio = _precoMedio;

                    } else {
                        if (portifolio.Quantidade >= lancamento.Quantidade){
                            var _quantidadeTotal = portifolio.Quantidade - lancamento.Quantidade;
                            
                            if (_quantidadeTotal == 0)
                                portifolio.PrecoMedio = 0;
                            portifolio.Quantidade = _quantidadeTotal;
                        } else {
                            throw new Exception("Não pode ocorrer uma venda sem ter a quantidade do ativo.");
                        }
                    }                    
                    _portifolioRepo.Atualizar(portifolio);
                    lancamento.Contabilizado = true;
                } else {
                    await RecalcularPortifolioAcao(portifolio, lancamento);
                    _portifolioRepo.Atualizar(portifolio);
                }
            }
        }

        private async Task RecalcularPortifolioAcao(Portifolio portifolio, Lancamento lancamentoAtual) {
            var lancamentos = await _lancamentoRepo.GetAllLancamentosByCarteiraIdAtivoIdAsync(portifolio.CarteiraId, portifolio.AtivoId, false, false);

            portifolio.PrecoMedio = 0;
            portifolio.Quantidade = 0;

            foreach (Lancamento _lancamento in lancamentos)
            {
                if ((lancamentoAtual.DataDaOperacao.Date < _lancamento.DataDaOperacao.Date) && 
                     !lancamentoAtual.Contabilizado)
                {
                    lancamentoAtual.Contabilizado = true;

                    if (lancamentoAtual.TipoDeMovimentacao == TipoDeMovimentacao.Compra)
                    {
                        var _quantidadeTotal = portifolio.Quantidade + lancamentoAtual.Quantidade;
                        var _precoMedio = ( (portifolio.Quantidade * portifolio.PrecoMedio) + 
                                            (lancamentoAtual.Quantidade * lancamentoAtual.ValorDaOperacao)) / 
                                            _quantidadeTotal;
                        
                        portifolio.Quantidade = _quantidadeTotal;
                        portifolio.PrecoMedio = _precoMedio;

                    } else {
                        if (portifolio.Quantidade >= lancamentoAtual.Quantidade){
                            var _quantidadeTotal = portifolio.Quantidade - lancamentoAtual.Quantidade;
                            
                            if (_quantidadeTotal == 0)
                                portifolio.PrecoMedio = 0;
                            portifolio.Quantidade = _quantidadeTotal;
                        } else {
                            throw new Exception("Não pode ocorrer uma venda sem ter a quantidade do ativo.");
                        }
                    }
                }

                if (_lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Compra)
                {
                    var _quantidadeTotal = portifolio.Quantidade + _lancamento.Quantidade;
                    var _precoMedio = ( (portifolio.Quantidade * portifolio.PrecoMedio) + 
                                        (_lancamento.Quantidade * _lancamento.ValorDaOperacao)) / 
                                        _quantidadeTotal;
                    
                    portifolio.Quantidade = _quantidadeTotal;
                    portifolio.PrecoMedio = _precoMedio;

                } else if (_lancamento.TipoDeMovimentacao == TipoDeMovimentacao.Venda) {
                    if (portifolio.Quantidade >= _lancamento.Quantidade){
                        var _quantidadeTotal = portifolio.Quantidade - _lancamento.Quantidade;
                        
                        if (_quantidadeTotal == 0)
                                portifolio.PrecoMedio = 0;
                        portifolio.Quantidade = _quantidadeTotal;
                    } else {
                        throw new Exception("Não pode ocorrer uma venda sem ter a quantidade do ativo.");
                    }
                } 
                _lancamento.Contabilizado = true;
                _lancamentoRepo.Atualizar(_lancamento);
            }
        }
    }
}