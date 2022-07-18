using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories
{
    public class GeralRepo : IGeralRepo
    {
        private readonly InvestQContext _context;

        public GeralRepo(InvestQContext context)
        {
            _context = context;
        }
        public void Adicionar<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Atualizar<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void AtualizarVarias<T>(T[] entityarray) where T : class
        {
            _context.UpdateRange(entityarray);
        }

        public void Deletar<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeletarVarias<T>(T[] entityarray) where T : class
        {
            _context.RemoveRange(entityarray);
        }

        public async Task<bool> SalvarMudancasAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}