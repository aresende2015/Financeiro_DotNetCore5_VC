using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.Clientes;
using InvestQ.Data.Paginacao;
using InvestQ.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.Clientes
{
    public class LancamentoRepo : GeralRepo, ILancamentoRepo
    {
        private readonly InvestQContext _context;

        public LancamentoRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PageList<Lancamento>> GetAllLancamentosByCarteiraIdAsync(Guid carteiraId, PageParams pageParams)
        {
            IQueryable<Lancamento> query = _context.Lancamentos;

            query = query.Include(l => l.Ativo);
            
            query = query.Include(l => l.Carteira);

            query = query.AsNoTracking()
                         .OrderByDescending(l => l.DataDaOperacao)
                         .Where(l => l.CarteiraId == carteiraId && l.Ativo.CodigoDoAtivo.Contains(pageParams.Term));

            return await PageList<Lancamento>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Lancamento[]> GetAllLancamentosByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId, PageParams pageParams, bool includeCarteira, bool includeAtivo)
        {
            IQueryable<Lancamento> query = _context.Lancamentos;
            if (includeCarteira)
                query = query.Include(l => l.Carteira);

            if (includeAtivo)
                query = query.Include(l => l.Ativo);
            
            if (includeAtivo) {
                query = query.AsNoTracking()
                            .OrderBy(l => l.DataDaOperacao).ThenBy(l => l.TipoDeMovimentacao)
                            .Where(l => (l.CarteiraId == carteiraId && l.AtivoId == ativoId) &&
                                         l.Ativo.CodigoDoAtivo.Contains(pageParams.Term));
            } else {
                query = query.AsNoTracking()
                        .OrderBy(l => l.DataDaOperacao).ThenBy(l => l.TipoDeMovimentacao)
                        .Where(l => l.CarteiraId == carteiraId
                                && l.AtivoId == ativoId);                
            }

            return await query.ToArrayAsync();
        }

        public async Task<Lancamento[]> GetAllLancamentosByCarteiraIdAtivoIdSemPaginacaoAsync(Guid carteiraId, Guid ativoId, bool includeCarteira, bool includeAtivo)
        {
            IQueryable<Lancamento> query = _context.Lancamentos;
            if (includeCarteira)
                query = query.Include(l => l.Carteira);

            if (includeAtivo)
                query = query.Include(l => l.Ativo);

            query = query.AsNoTracking()
                    .OrderBy(l => l.DataDaOperacao).ThenBy(l => l.TipoDeMovimentacao)
                    .Where(l => l.CarteiraId == carteiraId
                            && l.AtivoId == ativoId);                

            return await query.ToArrayAsync();
        }

        public DateTime GetDataLancamentoByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId)
        {
            DateTime maiorDataDeOperacao = _context.Lancamentos
                                                   .Where(l => l.CarteiraId == carteiraId
                                                             && l.AtivoId == ativoId)
                                                    .Max(l => l.DataDaOperacao);
            
           
            return maiorDataDeOperacao;
        }

        public async Task<Lancamento> GetLancamentoByIdAsync(Guid id)
        {
            IQueryable<Lancamento> query = _context.Lancamentos;

            query = query.Include(l => l.Ativo);
            
            query = query.Include(l => l.Carteira);

            query = query.AsNoTracking()
                         .OrderBy(l => l.DataDaOperacao)
                         .Where(l => l.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public bool GetPossuiLancamentoByCarteiraId(Guid carteiraId)
        {
            bool retorno = _context.Lancamentos
                                    .Where(l => l.CarteiraId == carteiraId)
                                    .Count() > 0;
            return retorno;
        }
    }
}