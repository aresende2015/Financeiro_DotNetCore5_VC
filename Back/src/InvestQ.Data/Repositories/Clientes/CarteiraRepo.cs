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
    public class CarteiraRepo : GeralRepo, ICarteiraRepo
    {
        private readonly InvestQContext _context;

        public CarteiraRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Carteira[]> GetAllCarteirasAsync(bool includeCliente, bool includeCorretora)
        {
            IQueryable<Carteira> query = _context.Carteiras;

            if (includeCliente)
                query = query.Include(c => c.Cliente);
            
            if (includeCorretora)
                query = query.Include(c => c.Corretora);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Descricao);

            return await query.ToArrayAsync();
        }

        public async Task<Carteira[]> GetAllCarteirasByClienteId(Guid clienteId, bool includeCliente, bool includeCorretora)
        {
            IQueryable<Carteira> query = _context.Carteiras;

            if (includeCliente)
                query = query.Include(c => c.Cliente);
            
            if (includeCorretora)
                query = query.Include(c => c.Corretora);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Descricao)
                         .Where(c => c.ClienteId == clienteId);

            return await query.ToArrayAsync();
        }

        public async Task<Carteira[]> GetAllCarteirasByCorretoraId(Guid corretoraId, bool includeCliente, bool includeCorretora)
        {
            IQueryable<Carteira> query = _context.Carteiras;

            if (includeCliente)
                query = query.Include(c => c.Cliente);
            
            if (includeCorretora)
                query = query.Include(c => c.Corretora);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Descricao)
                         .Where(c => c.CorretoraId == corretoraId);

            return await query.ToArrayAsync();
        }

        public async Task<Carteira> GetCarteiraByIdAsync(Guid id, bool includeCliente, bool includeCorretora)
        {
            IQueryable<Carteira> query = _context.Carteiras;

            if (includeCliente)
                query = query.Include(c => c.Cliente);
            
            if (includeCorretora)
                query = query.Include(c => c.Corretora);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Descricao)
                         .Where(c => c.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public bool GetPossuiCarteiraByClienteId(Guid clienteId)
        {
            bool retorno = _context.Carteiras
                                    .Where(c => c.ClienteId == clienteId)
                                    .Count() > 0;
            return retorno;
        }
    }
}