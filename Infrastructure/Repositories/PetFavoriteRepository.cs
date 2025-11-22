using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PetFavoriteRepository(AppDataContext context) : Repository<PetFavorite>(context), IPetFavoriteRepository
{
    
    public async Task<PetFavorite?> GetByPetIdAndUserId(Guid petId, string userId)
    {
        return await _context.PetFavorites.FirstOrDefaultAsync(x => x.ClerkId == userId && x.PetId == petId);
    }
}
