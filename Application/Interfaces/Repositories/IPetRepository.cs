using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<List<Pet>> GetAllPetsByUserId(string userId);
        Task<Pet> GetByPetIdAsync(Guid id);
    }
}
