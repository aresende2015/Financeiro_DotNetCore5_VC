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
    public class TipoDeAtivoRepo : GeralRepo, ITipoDeAtivoRepo
    {
        private readonly InvestQContext _context;
        public TipoDeAtivoRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }
        public async Task<TipoDeAtivo[]> GetAllTiposDeAtivosAsync(bool includeAtivo)
        {
            IQueryable<TipoDeAtivo> query = _context.TiposDeAtivos;

            //if (includeAtivo)
            //    query = query.Include(ta => ta.Ativos)

            query = query.AsNoTracking()
                         .OrderBy(ta => ta.Id);

            return await query.ToArrayAsync();
        }

        public async Task<TipoDeAtivo> GetTipoDeAtivoByDescricaoAsync(string descricao, bool includeAtivo)
        {
            IQueryable<TipoDeAtivo> query = _context.TiposDeAtivos;

            //if (includeAtivo)
            //    query = query.Include(ta => ta.Ativos);

            query = query.AsNoTracking()
                         .OrderBy(ta => ta.Descricao);

            return await query.FirstOrDefaultAsync(ta => ta.Descricao == descricao);
        }

        public async Task<TipoDeAtivo> GetTipoDeAtivoByIdAsync(int id, bool includeAtivo)
        {
            IQueryable<TipoDeAtivo> query = _context.TiposDeAtivos;

            //if (includeAtivo)
            //    query = query.Include(ta => ta.Ativos);

            query = query.AsNoTracking()
                         .OrderBy(ta => ta.Id)
                         .Where(ta => ta.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}