using Application.DTOs.Pet;
using Application.Helpers;
using Application.Interfaces.Repositories;
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


        public PetService(IPetRepository petRepository, IMapper mapper, IFileStorageService fileStorageService, IRepository<PetImages> petImagesRepository)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _petImagesRepository = petImagesRepository;
        }

        public async Task<Result<PetDto>> CreatePetAsync(CreatePetDto model)
        {
            //var pet = _mapper.Map<Pet>(model);
            var pet = new Pet();

            pet.SetPet(model.UserId, model.Name,
                model.Size, model.Bio, model.History, model.Address, model.Neighborhood, model.CEP,
                model.CEP, model.City, model.Number, model.Age, model.Complement);

            pet.SetTypeAndBreed(model.Type.Id, model.Breed.Id);

            await _petRepository.AddAsync(pet);

            //if (model.PetImages.Any())
            //{
            //    foreach (var item in model.PetImages)
            //    {                    
            //        var url = _fileStorageService.UploadAsync(item, "pets");
            //        var petImages = new PetImages(model.UserId, item.FileName, pet.Id);
            //        await _petImagesRepository.AddAsync(petImages);
            //    }
            //}
            return Result<PetDto>.Ok(new PetDto { Id = pet.Id });
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

        public async Task<Result<List<PetDto>>> GetAllPetsAsync(string userId)
        {
            var pets = await _petRepository.GetAllPetsByUserId(userId);
            var petDtos = _mapper.Map<List<PetDto>>(pets);
            return Result<List<PetDto>>.Ok(petDtos);
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
            var pet = await _petRepository.GetByPetIdAsync(id);
            if (pet == null)
                return Result<PetDto>.Fail("Pet not found");
            return Result<PetDto>.Ok(_mapper.Map<PetDto>(pet));
        }

        public async Task<Result<PetDto>> UpdatePetAsync(Guid id, UpdatePetDto model)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
                return Result<PetDto>.Fail("Pet not found");

            // Update pet properties
            pet.SetPet(model.UserId, model.Name,
                model.Size, model.Bio, model.History, model.Address, model.Neighborhood, model.CEP,
                model.CEP, model.City, model.Number, model.Age, model.Complement);
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
    }
}
