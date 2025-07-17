using Application.DTOs.Pet;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PetController : BaseController
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePetDto request)
        {
            var result = await _petService.CreatePetAsync(request);
            return HandleResult(result);
        }

        [HttpPost("images")]
        public async Task<IActionResult> Images([FromForm] CreatePetImagesDto request)
        {
            var result = await _petService.CreatePetImages(request);
            return HandleResult(result);
        }

        [HttpGet("GetAllPetsByUser")]
        [Authorize]
        public async Task<IActionResult> GetAllPetsByUser()
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var result = await _petService.GetAllPetsByUserIdAsync(userId);
            return HandleResult(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPets()
        {
            var result = await _petService.GetAllPetsAsync();
            return HandleResult(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePet(Guid id)
        {
            var result = await _petService.DeletePetAsync(id);
            return HandleResult(result);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPet(Guid id)
        {
            var result = await _petService.GetPetAsync(id);
            return HandleResult(result);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePet([FromBody] UpdatePetDto request, Guid id)
        {
            var result = await _petService.UpdatePetAsync(id, request);
            return HandleResult(result);
        }

        [Authorize]
        [HttpDelete("{petId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImageFromPet(Guid petId, Guid imageId)
        {
                var result = await _petService.DeleteImageFromPetAsync(petId, imageId);
            return HandleResult(result);
        }
    }
}
