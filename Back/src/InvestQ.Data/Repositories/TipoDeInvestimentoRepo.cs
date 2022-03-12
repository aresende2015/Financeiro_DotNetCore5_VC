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
    public class TipoDeInvestimentoRepo : GeralRepo, ITipoDeInvestimentoRepo
    {
        private readonly InvestQContext _context;
        public TipoDeInvestimentoRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }
        public async Task<TipoDeInvestimento[]> GetAllTipoDeInvestimentoAsync()
        {
            IQueryable<TipoDeInvestimento> query = _context.TiposDeInvestimentos;

            query = query.AsNoTracking()
                         .OrderBy(ti => ti.Id);

            return await query.ToArrayAsync();
        }

        public async Task<TipoDeInvestimento> GetTipoDeInvestimentoByIdAsync(int id)
        {
            IQueryable<TipoDeInvestimento> query = _context.TiposDeInvestimentos;

            query = query.AsNoTracking()
                         .Where(ti => ti.Id == id);                         

            return await query.FirstOrDefaultAsync();
        }
    }
}