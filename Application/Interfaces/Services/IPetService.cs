using Application.DTOs.Pet;
using Domain.Entities;
using Application.Helpers;
namespace Application.Interfaces.Services
{
    public interface IPetService
    {
        Task<Result<PetDto>> CreatePetAsync(CreatePetDto model);
        Task<Result<PetDto>> GetPetAsync(Guid id);
        Task<Result<List<PetDto>>> GetAllPetsByUserIdAsync(string userId);
        Task<Result<List<PetDto>>> GetAllPetsAsync();
        Task<Result<bool>> DeletePetAsync(Guid id);
        Task<Result<PetDto>> UpdatePetAsync(Guid id, UpdatePetDto model);
        Task<Result<bool>> DeleteImageFromPetAsync(Guid petId, Guid imageId);
        Task<Result<bool>> CreatePetImages(CreatePetImagesDto request);
        Task<Result<bool>> SetIsFavoritePet(Guid petId, string userId);
    }
}
