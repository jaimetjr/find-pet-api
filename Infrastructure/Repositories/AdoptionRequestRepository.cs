using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class AdoptionRequestRepository(AppDataContext context) : Repository<AdoptionRequest>(context), IAdoptionRequestRepository
{
    public async Task<AdoptionRequest?> CheckAdoptionStatus(Guid petId, string userId)
    {
        return await _context.AdoptionRequests
            .FirstOrDefaultAsync(ar => ar.PetId == petId && ar.AdopterClerkId == userId && ar.Active);
    }

    /// <summary>
    /// Returns all adoption requests for pets owned by the authenticated user (requests they've received as a pet owner).
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<AdoptionRequest>> GetReceived(string userId)
    {
        var statusDesired = new List<AdoptionRequestStatus>
        {
            AdoptionRequestStatus.Submitted,
            AdoptionRequestStatus.UnderReview,
            AdoptionRequestStatus.Interview
        };

        var request = await _context.AdoptionRequests
                        .Where(ar => ar.OwnerClerkId == userId && ar.Active
                               && statusDesired.Contains(ar.Status))
                        .Include(ar => ar.Pet)
                        .Include(ar => ar.Adopter)
                        .ToListAsync();
        return request;
    }

    public async Task<IEnumerable<AdoptionRequest>> GetByAdopterIdAsync(string adopterClerkId)
    {
        var request = await _context.AdoptionRequests
                                    .Where(x => x.AdopterClerkId == adopterClerkId)
                                    .Include(x => x.Pet)
                                    .Include(x => x.Owner)
                                    .OrderByDescending(x => x.CreatedAt)
                                    .ToListAsync();
        return request;
    }
}
