using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDataContext _context;

        public UserRepository(AppDataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByProviderAsync(string providerKey, ProviderType providerType)
        {
            return await _context.Users
               .Include(u => u.Providers)
               .FirstOrDefaultAsync(u =>
                   u.Providers.Any(p => p.ProviderKey == providerKey && p.Type == providerType));
        }
        
    }
}
