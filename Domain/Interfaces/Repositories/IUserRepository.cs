using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<User?> GetByClerkIdAsync(string clerkId, CancellationToken ct = default);
    Task<User?> GetByProviderAsync(string providerKey, ProviderType providerType, CancellationToken ct = default);
    Task<List<User>> GetExpoTokenWithoutMe(string clerkId, CancellationToken ct = default);
}

