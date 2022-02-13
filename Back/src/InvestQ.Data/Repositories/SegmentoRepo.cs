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
    public class SegmentoRepo : GeralRepo, ISegmentoRepo
    {
        private readonly InvestQContext _context;
        public SegmentoRepo(InvestQContext context) : base(context)
        {
             _context = context;
        }

        public async Task<Segmento[]> GetSegmentosBySubsetorIdAsync(int subsetorId)
        {
            IQueryable<Segmento> query = _context.Segmentos;

            query = query.Include(s => s.Subsetor);
            
            query = query.AsNoTracking()
                        .Where(s => s.SubsetorId == subsetorId);

            return await query.ToArrayAsync();
        }

        public async Task<Segmento> GetSegmentoByIdsAsync(int subsetorId, int segmentoId)
        {
            IQueryable<Segmento> query = _context.Segmentos;

            query = query.AsNoTracking()
                        .Where(s => s.SubsetorId == subsetorId
                                && s.Id == segmentoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}