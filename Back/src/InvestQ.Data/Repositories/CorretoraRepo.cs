using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces;
using InvestQ.Domain.Entities;

namespace InvestQ.Data.Repositories
{
    public class CorretoraRepo : GeralRepo, ICorretoraRepo
    {
        private readonly InvestQContext _context;

        public CorretoraRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public Task<Corretora[]> GetAllCorretorasAsync(bool includeCliente = false)
        {
            throw new NotImplementedException();
        }

        public Task<Corretora[]> GetAllCorretorasByClienteId(int clienteId, bool includeCliente = false)
        {
            throw new NotImplementedException();
        }

        public Task<Corretora> GetCorretoraByDescricaoAsync(string descricao)
        {
            throw new NotImplementedException();
        }

        public Task<Corretora> GetCorretoraByIdAsync(int id, bool includeCliente = false)
        {
            throw new NotImplementedException();
        }
    }
}