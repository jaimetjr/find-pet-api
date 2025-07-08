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
        public Task<List<Pet>> GetAllPetsByUserId(string userId)
        {
            return _context.Pets.Where(p => p.UserId == userId).Include(x => x.Breed).Include(x => x.Type).Include(x => x.PetImages).ToListAsync();
        }

        public async Task<Pet> GetByPetIdAsync(Guid id)
        {
            return await _context.Pets.Include(x => x.Breed).Include(x => x.Type).Include(x => x.PetImages).FirstAsync(x => x.Id == id);
        }
    }
}
