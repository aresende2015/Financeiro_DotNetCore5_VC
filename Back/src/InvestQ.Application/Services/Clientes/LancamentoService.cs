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
    public class LancamentoService : ILancamentoService
    {
        private readonly ILancamentoRepo _lancamentoRepo;
        private readonly IMapper _mapper;

        public LancamentoService(ILancamentoRepo lancamentoRepo,
                                 IMapper mapper)
        {
            _lancamentoRepo = lancamentoRepo;
            _mapper = mapper;
        }
        public async Task<LancamentoDto> AdicionarLancamento(LancamentoDto model)
        {
            var lancamento = _mapper.Map<Lancamento>(model);

            if( await _lancamentoRepo.GetLancamentoByIdAsync(lancamento.Id) == null)
            {
                _lancamentoRepo.Adicionar(lancamento);

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

        public async Task<LancamentoDto[]> GetAllLancamentosByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId)
        {
            try
            {
                var lancamentos = await _lancamentoRepo.GetAllLancamentosByCarteiraIdAtivoIdAsync(carteiraId, ativoId);

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
    }
}