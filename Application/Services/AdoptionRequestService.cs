using Application.DTOs.AdoptionRequest;
using Application.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class AdoptionRequestService : IAdoptionRequestService
{
    private readonly IAdoptionRequestRepository _adoptionRequestRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;
    private readonly IPushService _pushService;
    private readonly IUserService _userService;

    public AdoptionRequestService(IAdoptionRequestRepository adoptionRequestRepository,
                                  IPetRepository petRepository,
                                  IMapper mapper,
                                  INotificationRepository notificationRepository,
                                  IPushService pushService,
                                  IUserService userService)
    {
        _adoptionRequestRepository = adoptionRequestRepository;
        _petRepository = petRepository;
        _mapper = mapper;
        _notificationRepository = notificationRepository;
        _pushService = pushService;
        _userService = userService;
    }

    public async Task<Result<AdoptionRequestDto>> CreateAdoptionRequest(CreateAdoptionRequestDto request, string clerkId)
    {
        var pet = await _petRepository.GetByPetIdAsync(request.PetId);
        if (pet is null)
            return Result<AdoptionRequestDto>.Fail("Pet not found.");
        var ownerClerkId = pet.User.ClerkId;
        
        if (ownerClerkId == clerkId)
            return Result<AdoptionRequestDto>.Fail("You cannot create an adoption request for your own pet.");

        var adoptions = await _adoptionRequestRepository.GetReceived(clerkId);

        if (adoptions.Count() > 5) 
            return Result<AdoptionRequestDto>.Fail("You have reached the maximum number of active adoption requests (5).");


        var adoptionRequest = new AdoptionRequest(request.PetId, clerkId, ownerClerkId, AdoptionRequestStatus.UnderReview, request.Message);

        await _adoptionRequestRepository.AddAsync(adoptionRequest);

        if (adoptionRequest.Id == Guid.Empty)
            return Result<AdoptionRequestDto>.Fail("Failed to create adoption request.");

        var notification = new Notification(clerkId, NotificationType.RequestInterview, "Nova solicitação de adoção", 
            $"Sua solicitação de adoção para o pet {pet.Name} foi criada com sucesso e está em análise.", 
            adoptionRequest.Id.ToString(), $"/adoption-requests/{adoptionRequest.Id}");

        await _notificationRepository.AddAsync(notification);

        var expoToken = await _userService.GetExpoPushTokenAsync(ownerClerkId);
        if (!string.IsNullOrEmpty(expoToken.Value))
        {
            var message = $"{pet.User.Name} quer adotar {pet.Name}";
            await _pushService.SendNotificationAsync(expoToken.Value, "Nova Solicitação de Adoção", message, new
            {
                screen = "adoption-request-detail?requestId=" +pet.Id,
                userId = clerkId,
            });
        }

        var mapping = _mapper.Map<AdoptionRequestDto>(adoptionRequest);
        return Result<AdoptionRequestDto>.Ok(mapping);
    }

    public async Task<Result<List<AdoptionRequestDto>>> GetAdoptionRequestsByAdopterIdAsync(string adopterClerkId)
    {
        var adoptionRequests = await _adoptionRequestRepository.GetByAdopterIdAsync(adopterClerkId);
        var mapping = _mapper.Map<List<AdoptionRequestDto>>(adoptionRequests);
        return Result<List<AdoptionRequestDto>>.Ok(mapping);
    }

    public async Task<Result<List<AdoptionRequestDto>>> GetAdoptionRequestsByOwnerIdAsync(string ownerClerkId)
    {
        var adoptionRequests = await _adoptionRequestRepository.GetReceived(ownerClerkId);
        var mapping = _mapper.Map<List<AdoptionRequestDto>>(adoptionRequests);
        return Result<List<AdoptionRequestDto>>.Ok(mapping);
    }

    public async Task<Result<AdoptionRequestDto>> GetAdoptionRequestByIdAsync(Guid adoptionRequestId)
    {
        var adoptionRequest = await _adoptionRequestRepository.GetSingleAsync(x => x.Id == adoptionRequestId);
        if (adoptionRequest is null)
            return Result<AdoptionRequestDto>.Fail("Adoption request not found.");
        var mapping = _mapper.Map<AdoptionRequestDto>(adoptionRequest);
        return Result<AdoptionRequestDto>.Ok(mapping);
    }

    public async Task<Result<AdoptionRequestDto?>> CheckAdoptionStatus(Guid petId, string clerkId)
    {
        var check = await _adoptionRequestRepository.CheckAdoptionStatus(petId, clerkId);
        var mapping = _mapper.Map<AdoptionRequestDto?>(check);
        return Result<AdoptionRequestDto?>.Ok(mapping);
    }

    public async Task<Result<AdoptionRequestDto?>> UpdateAdoptionRequestStatus(UpdateAdoptionRequestDto request)
    {
        var adoptionRequest = await _adoptionRequestRepository.GetByIdAsync(request.Id);
        if (adoptionRequest is null)
            throw new Exception("Adoption request not found.");
        
        if (request.Status == AdoptionRequestStatus.Rejected && string.IsNullOrWhiteSpace(request.RejectionMessage))
            return Result<AdoptionRequestDto?>.Fail("Rejection message is required when rejecting an adoption request.");

        adoptionRequest.SetStatus(request.Status, request.RejectionMessage);
        await _adoptionRequestRepository.Update(adoptionRequest);
        var mapping = _mapper.Map<AdoptionRequestDto?>(adoptionRequest);
        return Result<AdoptionRequestDto?>.Ok(mapping);
    }

    public async Task<Result<string>> DeleteAdoptionRequest(Guid id, string clerkId)
    {
        var adoptionRequest = await _adoptionRequestRepository.GetByIdAsync(id);

        if (adoptionRequest is null)
            return Result<string>.Fail("Adoption request not found.");

        adoptionRequest.DeleteAdoptionRequest();
        await _adoptionRequestRepository.Update(adoptionRequest);
        return Result<string>.Ok("Adoption request deleted successfully.");
    }
}
