using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Security;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto?> AuthWithEmailAsync(LoginUserDto login)
        {
            var user = await _userRepository.GetByEmailAsync(login.Email);
            if (user == null || user.Providers == null)
                return null;
            var provider = user.Providers.FirstOrDefault(x => x.Type == ProviderType.Password);
            if (provider == null || string.IsNullOrEmpty(provider.PasswordHash) || string.IsNullOrEmpty(provider.PasswordSalt))
                return null;

            bool valid = PasswordHasher.VerifyPassword(login.Password, provider.PasswordHash, provider.PasswordSalt);
            if (!valid)
                return null;
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> RegisterWithEmailAsync(RegisterUserDto register)
        {
            var existingUser = await _userRepository.GetByEmailAsync(register.Email);
            if (existingUser != null)
                throw new Exception("User already exists");

            var (hash, salt) = PasswordHasher.HashPassword(register.Password);

            var user = new User(register.Name, register.Email, register.Notifications);
            user.SetAdditionalInfo(register.Avatar, register.Phone, register.Location, register.Bio);

            var provider = new Provider(user.Id, ProviderType.Password, register.Email);
            provider.SetPassword(hash, salt);

            user.Providers.Add(provider);

            await _userRepository.AddAsync(user);

            return _mapper.Map<UserDto>(user);
        }
    }
}
