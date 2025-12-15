using Application.DTOs.AdoptionRequest;
using Application.Helpers;
using Domain.Interfaces.Repositories;
using Domain.Specifications.AdoptionRequest;
using Domain.Specifications.Pet;
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
        var petSpec = new PetByIdSpecification(request.PetId)
            .WithUser();
        var pet = await _petRepository.GetSingleAsync(petSpec);
        if (pet is null)
            return Result<AdoptionRequestDto>.Fail("Pet not found.");
        var ownerClerkId = pet.User.ClerkId;

        if (ownerClerkId == clerkId)
            return Result<AdoptionRequestDto>.Fail("You cannot create an adoption request for your own pet.");

        var adoptionSpec = new ActiveAdoptionRequestsSpecification()
            .ByAdopter(clerkId);
        var adoptions = await _adoptionRequestRepository.ListAsync(adoptionSpec);

        if (adoptions.Count > 5)
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
            var title = "Nova Solicitação de Adoção";
            await _pushService.SendNotificationAsync(expoToken.Value, title, message, new
            {
                screen = "adoption-requests?tab=received",
                userId = clerkId,
            });
        }

        var mapping = _mapper.Map<AdoptionRequestDto>(adoptionRequest);
        return Result<AdoptionRequestDto>.Ok(mapping);
    }

    public async Task<Result<List<AdoptionRequestDto>>> GetAdoptionRequestsByAdopterIdAsync(string adopterClerkId)
    {
        var spec = new AdoptionRequestByAdopterSpecification(adopterClerkId)
            .WithAllIncludes()
            .OrderedByCreatedDateDescending();
        var adoptionRequests = await _adoptionRequestRepository.ListAsync(spec);
        var mapping = _mapper.Map<List<AdoptionRequestDto>>(adoptionRequests);
        return Result<List<AdoptionRequestDto>>.Ok(mapping);
    }

    public async Task<Result<List<AdoptionRequestDto>>> GetAdoptionRequestsByOwnerIdAsync(string ownerClerkId)
    {
        var spec = new ActiveAdoptionRequestsSpecification()
            .ByOwner(ownerClerkId)
            .WithStatuses(AdoptionRequestStatus.Submitted, AdoptionRequestStatus.UnderReview, AdoptionRequestStatus.Interview)
            .WithPet()
            .WithAdopter()
            .WithOwner()
            .OrderedByCreatedDateDescending();
        var adoptionRequests = await _adoptionRequestRepository.ListAsync(spec);
        var mapping = _mapper.Map<List<AdoptionRequestDto>>(adoptionRequests);
        return Result<List<AdoptionRequestDto>>.Ok(mapping);
    }

    public async Task<Result<AdoptionRequestDto>> GetAdoptionRequestByIdAsync(Guid adoptionRequestId)
    {
        var spec = new AdoptionRequestByIdSpecification(adoptionRequestId).WithAllIncludes();
        var adoptionRequest = await _adoptionRequestRepository.GetSingleAsync(spec);
        if (adoptionRequest is null)
            return Result<AdoptionRequestDto>.Fail("Adoption request not found.");
        var mapping = _mapper.Map<AdoptionRequestDto>(adoptionRequest);
        return Result<AdoptionRequestDto>.Ok(mapping);
    }

    public async Task<Result<AdoptionRequestDto?>> CheckAdoptionStatus(Guid petId, string clerkId)
    {
        var spec = new AdoptionRequestByPetAndAdopterSpecification(petId, clerkId);
        var check = await _adoptionRequestRepository.GetSingleAsync(spec);
        var mapping = _mapper.Map<AdoptionRequestDto?>(check);
        return Result<AdoptionRequestDto?>.Ok(mapping);
    }

    public async Task<Result<AdoptionRequestDto?>> UpdateAdoptionRequestStatus(UpdateAdoptionRequestDto request)
    {
        var spec = new AdoptionRequestByIdSpecification(request.Id).WithAllIncludes();
        var adoptionRequest = await _adoptionRequestRepository.GetByIdAsync(request.Id);
        if (adoptionRequest is null)
            throw new Exception("Adoption request not found.");

        if (request.Status == AdoptionRequestStatus.Rejected && string.IsNullOrWhiteSpace(request.RejectionMessage))
            return Result<AdoptionRequestDto?>.Fail("Rejection message is required when rejecting an adoption request.");

        adoptionRequest.SetStatus(request.Status, request.RejectionMessage);
        await _adoptionRequestRepository.Update(adoptionRequest);

        var expoToken = await _userService.GetExpoPushTokenAsync(adoptionRequest.Owner.ClerkId);
        if (!string.IsNullOrEmpty(expoToken.Value))
        {
            string title = string.Empty;
            string message = string.Empty;
            if (adoptionRequest.Status == AdoptionRequestStatus.Rejected)
            {
                title = "Seu pedido para adoção foi rejeitado";
                message = "Seu pedido foi rejeitado, toque aqui para ver o motivo";
            }
            else if (adoptionRequest.Status == AdoptionRequestStatus.Approved)
            {
                title = "Seu pedido para adoção foi aprovado!";
                message = "Seu pedido para adoção foi aprovado!";
            }
            await _pushService.SendNotificationAsync(expoToken.Value, title, message, new
            {
                screen = "adoption-requests?tab=my-requests",
                //userId = clerkId,
            }); ;
        }

        var mapping = _mapper.Map<AdoptionRequestDto?>(adoptionRequest);
        return Result<AdoptionRequestDto?>.Ok(mapping);
    }

    public async Task<Result<string>> DeleteAdoptionRequest(Guid id, string clerkId)
    {
        var spec = new AdoptionRequestByIdSpecification(id).WithAllIncludes();

        var adoptionRequest = await _adoptionRequestRepository.GetSingleAsync(spec);

        if (adoptionRequest is null)
            return Result<string>.Fail("Adoption request not found.");

        adoptionRequest.DeleteAdoptionRequest();
        await _adoptionRequestRepository.Update(adoptionRequest);

        var expoToken = await _userService.GetExpoPushTokenAsync(adoptionRequest.Owner.ClerkId);
        if (!string.IsNullOrEmpty(expoToken.Value))
        {
            var pet = adoptionRequest.Pet;
            var gender = pet.Gender == PetGender.Male ? "o" : "a";
            var message = $"{adoptionRequest.Adopter.Name} excluiu a solicitação adotar {gender} {pet.Name}";
            var title = "Exclusão da lista de adoção";
            await _pushService.SendNotificationAsync(expoToken.Value, title, message, new
            {
                screen = $"pet-detail?petId={pet.Id}",
                userId = clerkId,
            });
        }


        return Result<string>.Ok("Adoption request deleted successfully.");
    }
}
