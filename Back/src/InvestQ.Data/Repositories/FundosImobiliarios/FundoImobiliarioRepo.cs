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
    public class FundoImobiliarioRepo : GeralRepo, IFundoImobiliarioRepo
    {
        private readonly InvestQContext _context;
        public FundoImobiliarioRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }
        public async Task<FundoImobiliario[]> GetAllFundosImobiliariosAsync()
        {
            IQueryable<FundoImobiliario> query = _context.FundosImobiliarios;

            query = query.Include(fi => fi.SegmentoAnbima)
                         .Include(fi => fi.TipoDeInvestimento)
                         .Include(fi => fi.AdministradorDeFundoImobiliario);

            query = query.AsNoTracking()
                         .OrderBy(fi => fi.Id);

            return await query.ToArrayAsync();
        }

        public async Task<FundoImobiliario> GetFundoImobiliarioByIdAsync(Guid id)
        {
            IQueryable<FundoImobiliario> query = _context.FundosImobiliarios;

            query = query.Include(fi => fi.SegmentoAnbima)
                         .Include(fi => fi.TipoDeInvestimento)
                         .Include(fi => fi.AdministradorDeFundoImobiliario);

            query = query.AsNoTracking()
                         .Where(fi => fi.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<FundoImobiliario> GetFundoImobiliarioByDescricaoAsync(string descricao)
        {
            IQueryable<FundoImobiliario> query = _context.FundosImobiliarios;

            query = query.Include(fi => fi.SegmentoAnbima)
                         .Include(fi => fi.TipoDeInvestimento)
                         .Include(fi => fi.AdministradorDeFundoImobiliario);

            query = query.AsNoTracking()
                         .Where(fi => fi.Descricao == descricao);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<FundoImobiliario[]> GetFundosImobliariosByAdministradorDeFundoImobiliarioIdAsync(Guid administradorDeFundoImobiliarioId)
        {
            IQueryable<FundoImobiliario> query = _context.FundosImobiliarios;

            query = query.Include(fi => fi.SegmentoAnbima)
                         .Include(fi => fi.TipoDeInvestimento)
                         .Include(fi => fi.AdministradorDeFundoImobiliario);

            query = query.AsNoTracking()
                         .Where(fi => fi.AdministradorDeFundoImobiliarioId == administradorDeFundoImobiliarioId);

            return await query.ToArrayAsync();
        }

        public async Task<FundoImobiliario[]> GetFundosImobliariosBySegmentoAnbimaIdAsync(Guid segmentoAnbimaId)
        {
            IQueryable<FundoImobiliario> query = _context.FundosImobiliarios;

            query = query.Include(fi => fi.SegmentoAnbima)
                         .Include(fi => fi.TipoDeInvestimento)
                         .Include(fi => fi.AdministradorDeFundoImobiliario);

            query = query.AsNoTracking()
                         .Where(fi => fi.SegmentoAnbimaId == segmentoAnbimaId);

            return await query.ToArrayAsync();
        }

        public async Task<FundoImobiliario[]> GetFundosImobliariosByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId)
        {
            IQueryable<FundoImobiliario> query = _context.FundosImobiliarios;

            query = query.Include(fi => fi.SegmentoAnbima)
                         .Include(fi => fi.TipoDeInvestimento)
                         .Include(fi => fi.AdministradorDeFundoImobiliario);

            query = query.AsNoTracking()
                         .Where(fi => fi.TipoDeInvestimentoId == tipoDeInvestimentoId);

            return await query.ToArrayAsync();
        }
    }
}