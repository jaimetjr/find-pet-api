using Domain.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(AppDataContext context) : Repository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _context.Users.Include(x => x.Providers).FirstOrDefaultAsync(u => u.Email == email, ct);
    }

    public async Task<User?> GetByClerkIdAsync(string clerkId, CancellationToken ct = default)
    {
        return await _context.Users.Include(x => x.Providers).FirstOrDefaultAsync(u => u.ClerkId == clerkId, ct);
    }

    public async Task<User?> GetByProviderAsync(string providerKey, ProviderType providerType, CancellationToken ct = default)
    {
        return await _context.Users
           .Include(u => u.Providers)
           .FirstOrDefaultAsync(u =>
               u.Providers.Any(p => p.ProviderKey == providerKey && p.Type == providerType), ct);
    }
    
    public async Task<List<User>> GetExpoTokenWithoutMe(string clerkId, CancellationToken ct = default)
    {
        return await _context.Users.Where(x => x.ClerkId != clerkId && x.Notifications && !string.IsNullOrEmpty(x.ExpoPushToken)).ToListAsync(ct);
    }
}
