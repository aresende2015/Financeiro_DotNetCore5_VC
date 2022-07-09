using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.Clientes;
using InvestQ.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.Clientes
{
    public class PortifolioRepo : GeralRepo, IPortifolioRepo
    {
        private readonly InvestQContext _context;

        public PortifolioRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Portifolio[]> GetAllPortifoliosByCarteiraIdAsync(Guid carteiraId)
        {
            IQueryable<Portifolio> query = _context.Portifolios;

            query = query.Include(p => p.Ativo);
            
            query = query.Include(p => p.Carteira);

            query = query.AsNoTracking()
                         .OrderBy(p => p.Ativo.CodigoDoAtivo)
                         .Where(p => p.CarteiraId == carteiraId);

            return await query.ToArrayAsync();
        }

        public async Task<Portifolio> GetPortifolioByCarteiraIdAtivoIdAsync(Guid carteiraId, Guid ativoId)
        {
            IQueryable<Portifolio> query = _context.Portifolios;

            query = query.Include(p => p.Ativo);
            
            query = query.Include(p => p.Carteira);
            
            query = query.AsNoTracking()
                        .Where(p => p.CarteiraId == carteiraId
                                && p.AtivoId == ativoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Portifolio> GetPortifolioByIdAsync(Guid id)
        {
            IQueryable<Portifolio> query = _context.Portifolios;

            query = query.Include(p => p.Ativo);
            
            query = query.Include(p => p.Carteira);

            query = query.AsNoTracking()
                         .Where(p => p.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}