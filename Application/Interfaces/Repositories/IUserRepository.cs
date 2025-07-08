using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByClerkIdAsync(string clerkId);
        Task<User?> GetByProviderAsync(string providerKey, ProviderType providerType);
    }
}
