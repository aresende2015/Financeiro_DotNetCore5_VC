using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.Clientes;
using InvestQ.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.Clientes
{
    public class CorretoraRepo : GeralRepo, ICorretoraRepo
    {
        private readonly InvestQContext _context;

        public CorretoraRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Corretora[]> GetAllCorretorasAsync(bool includeCliente = false)
        {
            IQueryable<Corretora> query = _context.Corretoras;

            if (includeCliente)
                query = query.Include(c => c.ClientesCorretoras)
                             .ThenInclude(cc => cc.Cliente);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Corretora[]> GetAllCorretorasByClienteId(int clienteId, bool includeCliente)
        {
            IQueryable<Corretora> query = _context.Corretoras;

            if (includeCliente)
                query = query.Include(c => c.ClientesCorretoras)
                             .ThenInclude(cc => cc.Cliente);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Id)
                         .Where(c => c.ClientesCorretoras.Any(cc =>cc.ClienteId == clienteId));

            return await query.ToArrayAsync();
        }

        public async Task<Corretora> GetCorretoraByDescricaoAsync(string descricao, bool includeCliente)
        {
            IQueryable<Corretora> query = _context.Corretoras;

            if (includeCliente)
                query = query.Include(c => c.ClientesCorretoras)
                             .ThenInclude(cc => cc.Cliente);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Descricao);

            return await query.FirstOrDefaultAsync(c => c.Descricao == descricao);
        }

        public async Task<Corretora> GetCorretoraByIdAsync(int id, bool includeCliente = false)
        {
            IQueryable<Corretora> query = _context.Corretoras;

            if (includeCliente)
                query = query.Include(c => c.ClientesCorretoras)
                             .ThenInclude(cc => cc.Cliente);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Id)
                         .Where(c => c.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}