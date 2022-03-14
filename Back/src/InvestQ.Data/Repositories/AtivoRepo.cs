using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces;
using InvestQ.Domain.Entities;
using InvestQ.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories
{
    public class AtivoRepo : GeralRepo, IAtivoRepo
    {
        private readonly InvestQContext _context;
        public AtivoRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Ativo[]> GetAllAtivosByTipoDeAtivoAsync(TipoDeAtivo tipoDeAtivo)
        {
            IQueryable<Ativo> query = _context.Ativos;

            switch (tipoDeAtivo)
            {
                case TipoDeAtivo.Acao:
                    query = query.Include(a => a.Acao);
                    break;
                case TipoDeAtivo.FundoImobiliario:
                    query = query.Include(a => a.FundoImobiliario);
                    break;
                case TipoDeAtivo.TesouroDireto:
                    query = query.Include(a => a.TesouroDireto);
                    break;
            }                         

            query = query.AsNoTracking()
                         .Where(a => a.TipoDeAtivo == tipoDeAtivo);

            return await query.ToArrayAsync();
        }

        public async Task<Ativo> GetAtivoByIdAsync(int id)
        {
            IQueryable<Ativo> query = _context.Ativos;

            query = query.AsNoTracking()
                         .Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Ativo> GetAtivoByIdsAsync(int id, TipoDeAtivo tipoDeAtivo)
        {
            IQueryable<Ativo> query = _context.Ativos;

            switch (tipoDeAtivo)
            {
                case TipoDeAtivo.Acao:
                    query = query.Include(a => a.Acao);
                    break;
                case TipoDeAtivo.FundoImobiliario:
                    query = query.Include(a => a.FundoImobiliario);
                    break;
                case TipoDeAtivo.TesouroDireto:
                    query = query.Include(a => a.TesouroDireto);
                    break;
            }  

            query = query.AsNoTracking()
                         .Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}