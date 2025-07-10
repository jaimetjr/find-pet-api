using Application.DTOs.User;
using Application.Interfaces.Services;
using Application.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseController
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
            var result = await _userService.RegisterWithEmailAsync(dto);
            return HandleResult(result);
        }

        [HttpGet("me/{id}")]
        public async Task<IActionResult> GetMe(string id)
        {
            var user = await _userService.GetByClerkIdAsync(id);

            if (user == null)
                return NotFoundResult();

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [Authorize]
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromForm] UpdateUserDto dto)
        {
            var result = await _userService.UpdateUserAsync(id, dto);
            return HandleResult(result, user => Ok(user));
        }
    }
}
