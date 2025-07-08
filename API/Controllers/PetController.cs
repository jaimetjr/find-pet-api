using Application.DTOs.Pet;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePetDto request)
        {

            try
            {
                var pet = await _petService.CreatePetAsync(request);
                return Ok(pet);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("images")]
        public async Task<IActionResult> Images([FromForm] CreatePetImagesDto request)
        {
            try
            {
                var result = await _petService.CreatePetImages(request);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetAllPets()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var pets = await _petService.GetAllPetsAsync(userId);
                return Ok(pets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePet(Guid id)
        {
            try
            {
                var result = await _petService.DeletePetAsync(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPet(Guid id)
        {
            try
            {
                var pet = await _petService.GetPetAsync(id);
                if (pet.Success)
                    return Ok(pet);
                return NotFound(pet.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePet([FromBody] UpdatePetDto request, Guid id)
        {
            try
            {
                // Assuming you have an UpdatePetAsync method in your service
                var pet = await _petService.UpdatePetAsync(id, request);
                if (pet.Success)
                    return Ok(pet);
                return BadRequest(pet.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("${petId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImageFromPet(Guid petId, Guid imageId)
        {
            try
            {
                // Assuming you have a method to delete images from pets
                var result = await _petService.DeleteImageFromPetAsync(petId, imageId);
                if (result.Success)
                    return Ok(result);
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
