using Application.DTOs.User;
using Application.Interfaces.Services;
using Application.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController(IUserService userService, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
        {
            _userService = userService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var user = await _userService.RegisterWithEmailAsync(dto);
            if (user == null)
                return BadRequest(new { message = "Registration failed. Please check your details." });

            var token = _jwtTokenGenerator.GenerateToken(user);
            var userDto = _mapper.Map<UserDto>(user);

            var response = new AuthResponseDto
            {
                Token = token,
                User = userDto
            };
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var user = await _userService.AuthWithEmailAsync(dto);
            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var token = _jwtTokenGenerator.GenerateToken(user);
            var userDto = _mapper.Map<UserDto>(user);

            var response = new AuthResponseDto
            {
                User = userDto,
                Token = token
            };

            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
    }
}
