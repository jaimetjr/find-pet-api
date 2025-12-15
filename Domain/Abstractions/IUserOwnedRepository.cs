using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions;

public interface IUserOwnedRepository<T> where T : class, IHasClerkId
{
    Task<List<T>> ListAsync(string clerkId, ISpecification<T>? spec = null, CancellationToken ct = default);
    Task<T?> SingleOrDefaultAsync(string clerkId, ISpecification<T> spec, CancellationToken ct = default);
}
