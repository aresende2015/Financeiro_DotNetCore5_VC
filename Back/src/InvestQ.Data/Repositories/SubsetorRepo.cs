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

        public async Task<Subsetor> GetSubsetorByIdsAsync(int setorId, int subsetorId)
        {
            IQueryable<Subsetor> query = _context.Subsetores;

            query = query.AsNoTracking()
                        .Where(subsetor => subsetor.SetorId == setorId
                                && subsetor.Id == subsetorId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Subsetor[]> GetSubsetoresBySetorIdAsync(int setorId)
        {
            IQueryable<Subsetor> query = _context.Subsetores;

            query = query.Include(ss => ss.Setor).Include(sg => sg.Segmentos);
                         
            
            query = query.AsNoTracking()
                        .Where(subsetor => subsetor.SetorId == setorId);

            return await query.ToArrayAsync();
        }
    }
}