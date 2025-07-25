using Application.DTOs.User;
using Domain.Entities;
using Application.Helpers;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Result<string>> RegisterWithEmailAsync(RegisterUserDto register);
        Task<User?> AuthWithEmailAsync(LoginUserDto login);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByClerkIdAsync(string clerkId);
        Task<Result<UserDto>> UpdateUserAsync(Guid id, UpdateUserDto dto);
        Task<Result<UserDto>> UpdateExpoPushTokenAsync(string clerkId, string expoPushToken);
        Task<Result<string?>> GetExpoPushTokenAsync(string userClerkId);
    }
}
