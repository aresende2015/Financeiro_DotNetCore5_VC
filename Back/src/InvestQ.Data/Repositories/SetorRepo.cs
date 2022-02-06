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
    public class SetorRepo : GeralRepo, ISetorRepo
    {
        private readonly InvestQContext _context;
        public SetorRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Setor[]> GetAllSetoresAsync(bool includeSubsetor)
        {
            IQueryable<Setor> query = _context.Setores;

            if (includeSubsetor)
                query = query.Include(s => s.Subsetores)
                             .ThenInclude(ss => ss.Segmentos);

            query = query.AsNoTracking()
                         .OrderBy(s => s.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Setor> GetSetorByDescricaoAsync(string descricao, bool includeSubsetor)
        {
            IQueryable<Setor> query = _context.Setores;

            if (includeSubsetor)
                query = query.Include(s => s.Subsetores)
                             .ThenInclude(ss => ss.Segmentos);

            query = query.AsNoTracking()
                         .OrderBy(s => s.Descricao);

            return await query.FirstOrDefaultAsync(s => s.Descricao == descricao);
        }

        public async Task<Setor> GetSetorByIdAsync(int id, bool includeSubsetor)
        {
            IQueryable<Setor> query = _context.Setores;

            if (includeSubsetor)
                query = query.Include(s => s.Subsetores)
                             .ThenInclude(ss => ss.Segmentos);

            query = query.AsNoTracking()
                         .OrderBy(s => s.Id)
                         .Where(s => s.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}