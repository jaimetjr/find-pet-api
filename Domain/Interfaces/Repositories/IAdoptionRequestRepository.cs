using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IAdoptionRequestRepository : IRepository<AdoptionRequest>
{
    Task<IEnumerable<AdoptionRequest>> GetReceived(string userId, CancellationToken ct = default);
    Task<AdoptionRequest?> CheckAdoptionStatus(Guid petId, string userId, CancellationToken ct = default);
    Task<IEnumerable<AdoptionRequest>> GetByAdopterIdAsync(string adopterClerkId, CancellationToken ct = default);
}

