using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.Ativos;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.Ativos
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

        public async Task<Ativo> GetAtivoByTipoDeAtivoDescricaoAsync(TipoDeAtivo tipoDeAtivo, string descricao)
        {
            IQueryable<Ativo> query = _context.Ativos;

            switch (tipoDeAtivo)
            {
                case TipoDeAtivo.Acao:
                    query = query.Include(a => a.Acao);
                    query = query.AsNoTracking()
                                 .Where(a => a.TipoDeAtivo == tipoDeAtivo &&
                                             a.Acao.Descricao == descricao);
                    break;
                case TipoDeAtivo.FundoImobiliario:
                    query = query.Include(a => a.FundoImobiliario);
                    query = query.AsNoTracking()
                                 .Where(a => a.TipoDeAtivo == tipoDeAtivo &&
                                             a.FundoImobiliario.Descricao == descricao);
                    break;
                case TipoDeAtivo.TesouroDireto:
                    query = query.Include(a => a.TesouroDireto);
                    query = query.AsNoTracking()
                                 .Where(a => a.TipoDeAtivo == tipoDeAtivo &&
                                             a.TesouroDireto.Descricao == descricao);
                    break;
            }    

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Ativo> GetAtivoByIdAsync(Guid id)
        {
            IQueryable<Ativo> query = _context.Ativos;

            query = query.AsNoTracking()
                         .Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Ativo> GetAtivoByIdsAsync(Guid id, TipoDeAtivo tipoDeAtivo)
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

        public async Task<Ativo> GetAtivoByTesouroDiretoIdAsync(Guid tesouroDiretoId)
        {
            IQueryable<Ativo> query = _context.Ativos;

            query = query.AsNoTracking()
                         .Where(a => a.TesouroDiretoId == tesouroDiretoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}