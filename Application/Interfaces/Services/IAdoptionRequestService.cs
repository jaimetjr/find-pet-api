using Application.DTOs.AdoptionRequest;
using Application.Helpers;

namespace Application.Interfaces.Services;

public interface IAdoptionRequestService
{
    Task<Result<AdoptionRequestDto>> CreateAdoptionRequest(CreateAdoptionRequestDto request, string clerkId);
    Task<Result<List<AdoptionRequestDto>>> GetAdoptionRequestsByAdopterIdAsync(string adopterClerkId);
    Task<Result<List<AdoptionRequestDto>>> GetAdoptionRequestsByOwnerIdAsync(string ownerClerkId);
    Task<Result<AdoptionRequestDto>> GetAdoptionRequestByIdAsync(Guid adoptionRequestId);
    Task<Result<AdoptionRequestDto?>> CheckAdoptionStatus(Guid petId, string clerkId);
    Task<Result<AdoptionRequestDto?>> UpdateAdoptionRequestStatus(UpdateAdoptionRequestDto request);
    Task<Result<string>> DeleteAdoptionRequest(Guid id, string clerkId);
}
