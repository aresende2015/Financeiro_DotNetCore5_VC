using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.Acoes;
using InvestQ.Domain.Entities.Acoes;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.Acoes
{
    public class SubsetorRepo : GeralRepo, ISubsetorRepo
    {
        private readonly InvestQContext _context;
        public SubsetorRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Subsetor> GetSubsetorByIdAsync(Guid id)
        {
            IQueryable<Subsetor> query = _context.Subsetores;

            query = query.Include(ss => ss.Segmentos);

            query = query.AsNoTracking()
                        .Where(ss => ss.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Subsetor> GetSubsetorByIdsAsync(Guid setorId, Guid subsetorId)
        {
            IQueryable<Subsetor> query = _context.Subsetores;

            query = query.Include(ss => ss.Segmentos);
            
            query = query.AsNoTracking()
                        .Where(subsetor => subsetor.SetorId == setorId
                                && subsetor.Id == subsetorId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Subsetor[]> GetSubsetoresBySetorIdAsync(Guid setorId)
        {
            IQueryable<Subsetor> query = _context.Subsetores;

            query = query.Include(ss => ss.Setor).Include(sg => sg.Segmentos);
                         
            
            query = query.AsNoTracking()
                        .Where(subsetor => subsetor.SetorId == setorId);

            return await query.ToArrayAsync();
        }
    }
}