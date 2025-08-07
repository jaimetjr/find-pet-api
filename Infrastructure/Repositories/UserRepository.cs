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
    public class UserRepository(AppDataContext context) : Repository<User>(context), IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.Include(x => x.Providers).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByClerkIdAsync(string clerkId)
        {
            return await _context.Users.Include(x => x.Providers).FirstOrDefaultAsync(u => u.ClerkId == clerkId);
        }

        public async Task<User?> GetByProviderAsync(string providerKey, ProviderType providerType)
        {
            return await _context.Users
               .Include(u => u.Providers)
               .FirstOrDefaultAsync(u =>
                   u.Providers.Any(p => p.ProviderKey == providerKey && p.Type == providerType));
        }
        
        public async Task<List<User>> GetExpoTokenWithoutMe(string clerkId)
        {
            return await _context.Users.Where(x => x.ClerkId != clerkId && x.Notifications && !string.IsNullOrEmpty(x.ExpoPushToken)).ToListAsync();
        }
    }
}
