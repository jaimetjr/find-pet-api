using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDto?> RegisterWithEmailAsync(RegisterUserDto register);
        Task<UserDto?> AuthWithEmailAsync(LoginUserDto login);
    }
}
