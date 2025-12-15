using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPetFavoriteRepository : IRepository<PetFavorite>
{
    Task<PetFavorite?> GetByPetIdAndUserId(Guid petId, string userId, CancellationToken ct = default);
}

