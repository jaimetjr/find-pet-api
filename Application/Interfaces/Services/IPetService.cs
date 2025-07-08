using Application.DTOs.Pet;
using Domain.Entities;
using Application.Helpers;
namespace Application.Interfaces.Services
{
    public interface IPetService
    {
        Task<Result<PetDto>> CreatePetAsync(CreatePetDto model);
        Task<Result<PetDto>> GetPetAsync(Guid id);
        Task<Result<List<PetDto>>> GetAllPetsAsync(string userId);
        Task<Result<bool>> DeletePetAsync(Guid id);
        Task<Result<PetDto>> UpdatePetAsync(Guid id, UpdatePetDto model);
        Task<Result<bool>> DeleteImageFromPetAsync(Guid petId, Guid imageId);
        Task<Result<bool>> CreatePetImages(CreatePetImagesDto request);
    }
}
