using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Pet;
using Application.DTOs.User;
using Application.Interfaces.Services;
using Application.Helpers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestValidationController : BaseController
    {
        private readonly IValidationService _validationService;

        public TestValidationController(IValidationService validationService)
        {
            _validationService = validationService;
        }

        [HttpGet("messages")]
        public IActionResult GetLocalizedMessages([FromQuery] string culture = "en")
        {
            var messages = new
            {
                PetNameRequired = ValidationMessagesHelper.GetMessage("PetNameRequired", culture),
                AgeRequired = ValidationMessagesHelper.GetMessage("AgeRequired", culture),
                EmailRequired = ValidationMessagesHelper.GetMessage("EmailRequired", culture),
                PhoneRequired = ValidationMessagesHelper.GetMessage("PhoneRequired", culture),
                CEPFormat = ValidationMessagesHelper.GetMessage("CEPFormat", culture),
                CPFFormat = ValidationMessagesHelper.GetMessage("CPFFormat", culture)
            };

            return Ok(messages);
        }

        [HttpPost("test-pet-validation")]
        public async Task<IActionResult> TestPetValidation([FromBody] CreatePetDto dto)
        {
            var validationResult = await _validationService.ValidateAsync(dto);
            
            if (!_validationService.IsValid(validationResult))
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = _validationService.GetErrors(validationResult),
                    Message = "Validation failed"
                });
            }

            return Ok(new { Success = true, Message = "Validation passed" });
        }

        [HttpPost("test-user-validation")]
        public async Task<IActionResult> TestUserValidation([FromBody] RegisterUserDto dto)
        {
            var validationResult = await _validationService.ValidateAsync(dto);
            
            if (!_validationService.IsValid(validationResult))
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = _validationService.GetErrors(validationResult),
                    Message = "Validation failed"
                });
            }

            return Ok(new { Success = true, Message = "Validation passed" });
        }
    }
} 