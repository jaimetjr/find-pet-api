using Application.DTOs.User;
using Application.Interfaces.Services;
using Application.Security;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public AuthController(IUserService userService, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper, IAuthorizationService authorizationService)
        {
            _userService = userService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var user = await _userService.RegisterWithEmailAsync(dto);
            if (user != null && !user.Success)
            {
                var details = new ValidationProblemDetails(
                    new Dictionary<string, string[]>
                    {
                        { "errors", user.Errors.ToArray() }
                    });
                return BadRequest(details);
            }    

            return Ok(user);
        }

        [HttpGet("me/{id}")]
        public async Task<IActionResult> GetMe(string id)
        {
            var user = await _userService.GetByClerkIdAsync(id);

            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [Authorize]
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromForm] UpdateUserDto dto)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, dto);
            if (updatedUser == null)
                return NotFound(new { message = "User not found or update failed." });
            return Ok(updatedUser);
        }
    }
}
