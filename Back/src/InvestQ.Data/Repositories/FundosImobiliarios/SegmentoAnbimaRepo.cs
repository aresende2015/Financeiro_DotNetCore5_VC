using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.FundosImobiliarios;
using InvestQ.Domain.Entities.FundosImobiliarios;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.FundosImobiliarios
{
    public class SegmentoAnbimaRepo : GeralRepo, ISegmentoAnbimaRepo
    {
        private readonly InvestQContext _context;
        public SegmentoAnbimaRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }
        public async Task<SegmentoAnbima[]> GetAllSegmentosAnbimasAsync(bool includeFundoImobiliario)
        {
            IQueryable<SegmentoAnbima> query = _context.SegmentosAnbimas;

            if (includeFundoImobiliario)
                query = query.Include(sa => sa.FundosImobiliarios);

            query = query.AsNoTracking()
                         .OrderBy(sa => sa.Id);

            return await query.ToArrayAsync();
        }

        public async Task<SegmentoAnbima> GetSegmentoAnbimaByDescricaoAsync(string descricao, bool includeFundoImobiliario)
        {
            IQueryable<SegmentoAnbima> query = _context.SegmentosAnbimas;

            if (includeFundoImobiliario)
                query = query.Include(sa => sa.FundosImobiliarios);

            query = query.AsNoTracking()
                         .OrderBy(sa => sa.Descricao);

            return await query.FirstOrDefaultAsync(sa => sa.Descricao == descricao);
        }

        public async Task<SegmentoAnbima> GetSegmentoAnbimaByIdAsync(int id, bool includeFundoImobiliario)
        {
            IQueryable<SegmentoAnbima> query = _context.SegmentosAnbimas;

            if (includeFundoImobiliario)
                query = query.Include(sa => sa.FundosImobiliarios);

            query = query.AsNoTracking()
                         .OrderBy(sa => sa.Id)
                         .Where(sa => sa.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}