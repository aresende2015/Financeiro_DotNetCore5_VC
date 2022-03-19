using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.Identity;
using InvestQ.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.Identity
{
    public class UserRepo : GeralRepo, IUserRepo
    {
        private readonly InvestQContext _context;

        public UserRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                    .SingleOrDefaultAsync(user => user.UserName == username.ToLower());
        }
    }
}