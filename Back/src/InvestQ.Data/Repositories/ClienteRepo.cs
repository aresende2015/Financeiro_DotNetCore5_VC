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
    public class ClienteRepo : GeralRepo, IClienteRepo
    {
        private readonly InvestQContext _context;

        public ClienteRepo(InvestQContext context) : base(context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Cliente[]> GetAllClientesAsync(bool includeCorretora = false)
        {
            IQueryable<Cliente> query = _context.Clientes;

            if (includeCorretora)
                query = query.Include(c => c.ClientesCorretoras)
                             .ThenInclude(cc => cc.Corretora);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Cliente[]> GetAllClientesByCorretoraId(int corretoraId, bool includeCorretora = false)
        {
            IQueryable<Cliente> query = _context.Clientes;

            if (includeCorretora)
                query = query.Include(c => c.ClientesCorretoras)
                             .ThenInclude(cc => cc.Corretora);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Id)
                         .Where(c => c.ClientesCorretoras.Any(cc =>cc.CorretoraId == corretoraId));

            return await query.ToArrayAsync();
        }

        public async Task<Cliente> GetClienteByCpfAsync(string cpf)
        {
            IQueryable<Cliente> query = _context.Clientes;

            query = query.AsNoTracking()
                         .OrderBy(c => c.Cpf);

            return await query.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Cliente> GetClienteByIdAsync(int id, bool includeCorretora = false)
        {
            IQueryable<Cliente> query = _context.Clientes;

            if (includeCorretora)
                query = query.Include(c => c.ClientesCorretoras)
                             .ThenInclude(cc => cc.Corretora);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Id)
                         .Where(c => c.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}