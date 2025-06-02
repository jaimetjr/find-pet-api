using Application.DTOs.User;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> RegisterWithEmailAsync(RegisterUserDto register);
        Task<User?> AuthWithEmailAsync(LoginUserDto login);
        Task<User> GetByIdAsync(Guid id);
    }
}
