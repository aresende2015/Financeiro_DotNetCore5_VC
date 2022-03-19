using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.Clientes;
using InvestQ.Data.Paginacao;
using InvestQ.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.Clientes
{
    public class ClienteRepo : GeralRepo, IClienteRepo
    {
        private readonly InvestQContext _context;

        public ClienteRepo(InvestQContext context) : base(context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<PageList<Cliente>> GetAllClientesAsync(int userId, PageParams pageParams, bool includeCorretora)
        {
            IQueryable<Cliente> query = _context.Clientes;

            if (includeCorretora)
                query = query.Include(c => c.ClientesCorretoras)
                             .ThenInclude(cc => cc.Corretora);

            query = query.AsNoTracking()
                         .Where(c => ((c.Nome + ' ' + c.SobreNome).ToLower().Contains(pageParams.Term.ToLower()) ||
                                      c.Cpf.Contains(pageParams.Term)) &&
                                      c.UserId == userId)
                         .OrderBy(c => c.Id);

            return await PageList<Cliente>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<PageList<Cliente>> GetAllClientesByCorretoraId(int userId, PageParams pageParams, int corretoraId, bool includeCorretora)
        {
            IQueryable<Cliente> query = _context.Clientes;

            if (includeCorretora)
                query = query.Include(c => c.ClientesCorretoras)
                             .ThenInclude(cc => cc.Corretora);

            query = query.AsNoTracking()
                         .OrderBy(c => c.Id)
                         .Where(c => c.ClientesCorretoras.Any(cc =>cc.CorretoraId == corretoraId) && c.UserId == userId);

            return await PageList<Cliente>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Cliente> GetClienteByCpfAsync(int userId, string cpf, bool includeCorretora)
        {
            IQueryable<Cliente> query = _context.Clientes;

            query = query.AsNoTracking()
                         .Where(c => c.UserId == userId)
                         .OrderBy(c => c.Cpf);

            return await query.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Cliente> GetClienteByIdAsync(int userId, int id, bool includeCorretora)
        {
            IQueryable<Cliente> query = _context.Clientes;

            if (includeCorretora)
                query = query.Include(c => c.ClientesCorretoras)
                             .ThenInclude(cc => cc.Corretora);

            query = query.AsNoTracking()
                         .Where(c => c.Id == id && c.UserId == userId)
                         .OrderBy(c => c.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}