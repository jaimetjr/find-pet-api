using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PetRepository(AppDataContext context) : Repository<Pet>(context), IPetRepository
    {
        public async Task<List<Pet>> GetAllPetsByUserId(string clerkId)
        {
            return await _context.Pets.Where(p => p.ClerkId == clerkId).Include(x => x.Breed).Include(x => x.Type).Include(x => x.PetImages).Include(x => x.PetFavorites).ToListAsync();
        }

        public async Task<List<Pet>> GetAllPets()
        {
            return await _context.Pets.Include(x => x.Breed).Include(x => x.Type).Include(x => x.PetImages).Include(x => x.PetFavorites).ToListAsync();
        }

        public async Task<Pet> GetByPetIdAsync(Guid id)
        {
            return await _context.Pets.Include(x => x.Breed).Include(x => x.Type).Include(x => x.PetImages).Include(x => x.User).Include(x => x.PetFavorites).FirstAsync(x => x.Id == id);
        }
    }
}
