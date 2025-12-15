using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PetRepository(AppDataContext context) : Repository<Pet>(context), IPetRepository
    {
        public async Task<List<Pet>> GetAllPetsByUserId(string clerkId, CancellationToken ct = default)
        {
            return await _context.Pets.Where(p => p.ClerkId == clerkId).Include(x => x.Breed).Include(x => x.Type).Include(x => x.PetImages).Include(x => x.PetFavorites).ToListAsync(ct);
        }

        public async Task<List<Pet>> GetAllPets(CancellationToken ct = default)
        {
            return await _context.Pets.Include(x => x.Breed).Include(x => x.Type).Include(x => x.PetImages).Include(x => x.PetFavorites).ToListAsync(ct);
        }

        public async Task<Pet> GetByPetIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Pets.Include(x => x.Breed).Include(x => x.Type).Include(x => x.PetImages).Include(x => x.User).Include(x => x.PetFavorites).FirstAsync(x => x.Id == id, ct);
        }
    }
}
