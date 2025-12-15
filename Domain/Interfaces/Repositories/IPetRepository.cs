using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPetRepository : IRepository<Pet>
{
    Task<List<Pet>> GetAllPets(CancellationToken ct = default);
    Task<List<Pet>> GetAllPetsByUserId(string clerkId, CancellationToken ct = default);
    Task<Pet> GetByPetIdAsync(Guid id, CancellationToken ct = default);
}

