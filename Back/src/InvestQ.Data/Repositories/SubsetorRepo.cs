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
    public class SubsetorRepo : GeralRepo, ISubsetorRepo
    {
        private readonly InvestQContext _context;
        public SubsetorRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Subsetor[]> GetAllSubsetoresAsync(bool includeSegmento)
        {
            IQueryable<Subsetor> query = _context.Subsetores;

            if (includeSegmento)
                query = query.Include(ss => ss.Segmentos);

            query = query.AsNoTracking()
                         .OrderBy(ss => ss.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Subsetor> GetSubsetorByDescricaoAsync(string descricao, bool includeSegmento)
        {
            IQueryable<Subsetor> query = _context.Subsetores;

            if (includeSegmento)
                query = query.Include(ss => ss.Segmentos);

            query = query.AsNoTracking()
                         .OrderBy(ss => ss.Descricao);

            return await query.FirstOrDefaultAsync(ss => ss.Descricao == descricao);
        }

        public async Task<Subsetor> GetSubsetorByIdAsync(int id, bool includeSegmento)
        {
            IQueryable<Subsetor> query = _context.Subsetores;

            if (includeSegmento)
                query = query.Include(ss => ss.Segmentos);

            query = query.AsNoTracking()
                         .OrderBy(ss => ss.Id)
                         .Where(ss => ss.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}