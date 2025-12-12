using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IAdoptionRequestRepository : IRepository<AdoptionRequest>
{
    Task<IEnumerable<AdoptionRequest>> GetReceived(string userId);
    Task<AdoptionRequest?> CheckAdoptionStatus(Guid petId, string userId);
    Task<IEnumerable<AdoptionRequest>> GetByAdopterIdAsync(string adopterClerkId);
}
