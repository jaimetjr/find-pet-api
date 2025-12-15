using Application.DTOs.Pet;
using Application.Helpers;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Specifications.Pet;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IRepository<PetImages> _petImagesRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly IValidationService _validationService;
        private readonly ILoggingService _loggingService;
        private readonly IPetFavoriteRepository _petFavoriteRepository;

        public PetService(IPetRepository petRepository, IMapper mapper, IFileStorageService fileStorageService, IRepository<PetImages> petImagesRepository, IValidationService validationService, ILoggingService loggingService, IPetFavoriteRepository petFavoriteRepository)
        {
            _petRepository = petRepository;
            _petFavoriteRepository = petFavoriteRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _petImagesRepository = petImagesRepository;
            _validationService = validationService;
            _loggingService = loggingService;
        }

        public async Task<Result<PetDto>> CreatePetAsync(CreatePetDto model)
        {
            _loggingService.LogInformation("Creating pet for user {UserId}", model.UserId);
            
            // Validate the model
            var validationResult = await _validationService.ValidateAsync(model);
            if (!_validationService.IsValid(validationResult))
            {
                var errors = _validationService.GetErrors(validationResult);
                _loggingService.LogWarning("Pet creation validation failed for user {UserId}. Errors: {Errors}", 
                    model.UserId, string.Join(", ", errors));
                return Result<PetDto>.Fail(errors.ToArray());
            }

            try
            {
                var pet = new Pet();

                pet.SetPet(model.UserId, model.Name,
                    model.Size, model.Bio, model.History, model.Address, model.Neighborhood, model.CEP,
                    model.CEP, model.City, model.Number, model.Age, model.Gender, model.Complement);

                pet.SetTypeAndBreed(model.Type.Id, model.Breed.Id);

                await _petRepository.AddAsync(pet);

                _loggingService.LogInformation("Pet created successfully. PetId: {PetId}, UserId: {UserId}", 
                    pet.Id, model.UserId);

                return Result<PetDto>.Ok(new PetDto { Id = pet.Id });
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error creating pet for user {UserId}", ex, model.UserId);
                return Result<PetDto>.Fail("An error occurred while creating the pet");
            }
        }

        public async Task<Result<bool>> CreatePetImages(CreatePetImagesDto request)
        {
            var pet = await _petRepository.GetByIdAsync(request.PetId);
            if (pet == null)
                return Result<bool>.Fail("Pet not found");

            if (request.Images == null || !request.Images.Any())
                return Result<bool>.Fail("No images provided");

            foreach (var item in request.Images)
            {
                var url = await _fileStorageService.UploadAsync(item, "pets");
                var petImage = new PetImages(request.UserId, url, request.PetId);
                await _petImagesRepository.AddAsync(petImage);
            }

            return Result<bool>.Ok(true);
        }

        public async Task<Result<List<PetDto>>> GetAllPetsByUserIdAsync(string userId)
        {
            _loggingService.LogInformation("Retrieving all pets for user {UserId}", userId);
            
            try
            {
                var spec = new PetByUserIdSpecification(userId)
                    .WithAllIncludes();
                var pets = await _petRepository.ListAsync(spec);
                var petDtos = _mapper.Map<List<PetDto>>(pets);
                
                _loggingService.LogInformation("Retrieved {PetCount} pets for user {UserId}", 
                    petDtos.Count, userId);
                
                return Result<List<PetDto>>.Ok(petDtos);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error retrieving pets for user {UserId}", ex, userId);
                return Result<List<PetDto>>.Fail("An error occurred while retrieving pets");
            }
        }

        public async Task<Result<List<PetDto>>> GetAllPetsAsync()
        {
            _loggingService.LogInformation("Retrieving all pets");

            try
            {
                var spec = new PetWithImagesSpecification()
                    .WithBreed()
                    .WithType()
                    .WithFavorites();
                var pets = await _petRepository.ListAsync(spec);
                var petDtos = _mapper.Map<List<PetDto>>(pets);

                _loggingService.LogInformation("Retrieved {PetCount} pets",
                    petDtos.Count);

                return Result<List<PetDto>>.Ok(petDtos);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error retrieving pets", ex);
                return Result<List<PetDto>>.Fail("An error occurred while retrieving pets");
            }
        }


        public async Task<Result<bool>> DeletePetAsync(Guid id)
        {
            var pet = await _petRepository.GetByIdAsync(id);

            if (pet == null)
                return Result<bool>.Ok(false);

            await _petRepository.Remove(pet);
            return Result<bool>.Ok(true);
        }

        public async Task<Result<PetDto>> GetPetAsync(Guid id)
        {
            var spec = new PetByIdSpecification(id)
                .WithAllIncludes();
            var pet = await _petRepository.GetSingleAsync(spec);
            if (pet == null)
                return Result<PetDto>.Fail("Pet not found");
            return Result<PetDto>.Ok(_mapper.Map<PetDto>(pet));
        }

        public async Task<Result<PetDto>> UpdatePetAsync(Guid id, UpdatePetDto model)
        {
            // Validate the model
            var validationResult = await _validationService.ValidateAsync(model);
            if (!_validationService.IsValid(validationResult))
            {
                return Result<PetDto>.Fail(_validationService.GetErrors(validationResult).ToArray());
            }

            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
                return Result<PetDto>.Fail("Pet not found");

            // Update pet properties
            pet.SetPet(model.UserId, model.Name,
                model.Size, model.Bio, model.History, model.Address, model.Neighborhood, model.CEP,
                model.CEP, model.City, model.Number, model.Age, model.Gender, model.Complement);
            pet.SetTypeAndBreed(model.Type.Id, model.Breed.Id);

            await _petRepository.Update(pet);

            var updatedPetDto = _mapper.Map<PetDto>(pet);
            return Result<PetDto>.Ok(updatedPetDto);
        }

        public async Task<Result<bool>> DeleteImageFromPetAsync(Guid petId, Guid imageId)
        {
            var pet = await _petRepository.GetByIdAsync(petId);
            if (pet == null)
                return Result<bool>.Fail("Pet not found");
            var image = await _petImagesRepository.GetByIdAsync(imageId);
            if (image == null || image.PetId != petId)
                return Result<bool>.Fail("Image not found or does not belong to the specified pet");
            await _petImagesRepository.Remove(image);

            // delete from storage
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> SetIsFavoritePet(Guid petId, string userId)
        {
            var existingFavorite = await _petFavoriteRepository.GetByPetIdAndUserId(petId, userId);

            if (existingFavorite != null)
            {
                existingFavorite.ToggleFavorite();
                await _petFavoriteRepository.Update(existingFavorite);
            }
            else
            {
                var newFavorite = new PetFavorite();
                newFavorite.SetPetFavorite(userId, petId, true);
                await _petFavoriteRepository.AddAsync(newFavorite);
            }

            return Result<bool>.Ok(true);
        }
    }
}
