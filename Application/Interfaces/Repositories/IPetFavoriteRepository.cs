using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPetFavoriteRepository : IRepository<PetFavorite>
{
    Task<PetFavorite?> GetByPetIdAndUserId(Guid petId, string userId);
}
