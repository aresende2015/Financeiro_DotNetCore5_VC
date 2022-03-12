using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces;
using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories
{
    public class TesouroDiretoRepo : GeralRepo, ITesouroDiretoRepo
    {
        private readonly InvestQContext _context;
        public TesouroDiretoRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TesouroDireto[]> GetAllTeseourosDiretosAsync()
        {
            IQueryable<TesouroDireto> query = _context.TesourosDiretos;

            query = query.Include(td => td.TipoDeInvestimento);

            query = query.AsNoTracking()
                         .OrderBy(td => td.Id);

            return await query.ToArrayAsync();
        }

        public async Task<TesouroDireto[]> GetTeseourosDiretosByTipoDeInvestimentoIdAsync(int tipoDeInvestimentoId)
        {
            IQueryable<TesouroDireto> query = _context.TesourosDiretos;

            query = query.Include(td => td.TipoDeInvestimento);

            query = query.AsNoTracking()
                         .Where(td => td.TipoDeInvestimentoId == tipoDeInvestimentoId);

            return await query.ToArrayAsync();
        }

        public async Task<TesouroDireto> GetTesouroDiretoByDescricaoAsync(string descricao)
        {
            IQueryable<TesouroDireto> query = _context.TesourosDiretos;

            query = query.Include(td => td.TipoDeInvestimento);

            query = query.AsNoTracking()
                         .Where(td => td.Descricao == descricao);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TesouroDireto> GetTesouroDiretoByIdAsync(int id)
        {
            IQueryable<TesouroDireto> query = _context.TesourosDiretos;

            query = query.Include(td => td.TipoDeInvestimento);

            query = query.AsNoTracking()
                         .Where(td => td.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}