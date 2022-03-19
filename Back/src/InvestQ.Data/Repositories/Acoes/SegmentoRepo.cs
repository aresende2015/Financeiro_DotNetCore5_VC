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
    public class SegmentoRepo : GeralRepo, ISegmentoRepo
    {
        private readonly InvestQContext _context;
        public SegmentoRepo(InvestQContext context) : base(context)
        {
             _context = context;
        }

        public async Task<Segmento[]> GetSegmentosBySubsetorIdAsync(Guid subsetorId)
        {
            IQueryable<Segmento> query = _context.Segmentos;

            query = query.Include(s => s.Acoes)
                        .Include(s => s.Subsetor)
                        .ThenInclude(s => s.Setor);
            
            query = query.AsNoTracking()
                        .Where(s => s.SubsetorId == subsetorId);

            return await query.ToArrayAsync();
        }

        public async Task<Segmento> GetSegmentoByIdsAsync(Guid subsetorId, Guid segmentoId)
        {
            IQueryable<Segmento> query = _context.Segmentos;

            query = query.Include(s => s.Acoes)
                        .Include(s => s.Subsetor)
                        .ThenInclude(s => s.Setor);

            query = query.AsNoTracking()
                        .Where(s => s.SubsetorId == subsetorId
                                && s.Id == segmentoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}