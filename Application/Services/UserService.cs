using Application.DTOs.User;
using Application.Helpers;
using Domain.Interfaces.Repositories;
using Domain.Specifications.User;
using Application.Interfaces.Services;
using Application.Resources;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public UserService(IUserRepository userRepository, IMapper mapper, IFileStorageService fileStorageService, IValidationService validationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _validationService = validationService;
        }

        public async Task<User?> AuthWithEmailAsync(LoginUserDto login)
        {
            var spec = new UserByEmailSpecification(login.Email)
                .WithProviders();
            var user = await _userRepository.GetSingleAsync(spec);
            if (user == null || user.Providers == null)
                return null;

            return user;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;
            return user;
        }

        public async Task<User?> GetByClerkIdAsync(string clerkId)
        {
            var spec = new UserByClerkIdSpecification(clerkId)
                .WithProviders();
            var user = await _userRepository.GetSingleAsync(spec);
            if (user == null)
                return null;
            return user;
        }

        public async Task<Result<string>> RegisterWithEmailAsync(RegisterUserDto register)
        {
            var emailSpec = new UserByEmailSpecification(register.Email)
                .WithProviders();
            var existingUser = await _userRepository.GetSingleAsync(emailSpec);
            if (existingUser != null)
                return Result<string>.Fail(ValidationMessages.UserAlreadyExists);

            var user = new User(register.Email, register.Name, register.Phone, register.Notifications, register.CPF);
            user.SetAdditionalInfo(register.Phone, register.Bio, register.ClerkId, register.ContactType, register.BirthDate);
            user.SetAddressInformation(
                register.Address,
                register.Neighborhood,
                register.CEP,
                register.State,
                register.City,
                register.Complement,
                register.Number
            );

            if (register.Avatar != null)
            {
                var avatarUrl = await _fileStorageService.UploadAsync(register.Avatar, "users");
                user.SetAvatar(avatarUrl);
            }

            var provider = new Provider(user.Id, register.Provider, register.Email);

            user.Providers.Add(provider);
            await _userRepository.AddAsync(user);

            return Result<string>.Ok("Usuário inserido com sucesso");
        }

        public async Task<Result<UserDto>> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            try
            {
                // Validate the model
                var validationResult = await _validationService.ValidateAsync(dto);
                if (!_validationService.IsValid(validationResult))
                {
                    return Result<UserDto>.Fail(_validationService.GetErrors(validationResult).ToArray());
                }

                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    return Result<UserDto>.Fail("Usuário não encontrado");

                string avatar = string.Empty;
                if (dto.Avatar != null)
                {
                    var isDeleted = await _fileStorageService.DeleteAsync(dto.Avatar, "users");
                    if (isDeleted)
                        avatar = await _fileStorageService.UploadAsync(dto.Avatar, "users");
                }
                if (string.IsNullOrEmpty(avatar))
                    avatar = user.Avatar ?? "";

                user.UpdateProfile(avatar, dto.Phone, dto.Bio, dto.BirthDate, dto.CPF,
                                   dto.Address, dto.Neighborhood, dto.CEP, dto.State, dto.City, dto.Complement, dto.Number, dto.Notifications, dto.ContactType);

                await _userRepository.Update(user);

                var userDto = _mapper.Map<UserDto>(user);
                return Result<UserDto>.Ok(userDto);
            }
            catch (Exception ex)
            {
                return Result<UserDto>.Fail(ex.Message);
            }
        }

        public async Task<Result<UserDto>> UpdateExpoPushTokenAsync(string clerkId, string expoPushToken)
        {
            try
            {
                var spec = new UserByClerkIdSpecification(clerkId);
                var user = await _userRepository.GetSingleAsync(spec);
                if (user == null)
                    return Result<UserDto>.Fail("Usuário não encontrado");
                user.SetPushToken(expoPushToken);
                await _userRepository.Update(user);
                var userDto = _mapper.Map<UserDto>(user);
                return Result<UserDto>.Ok(userDto);
            }
            catch (Exception ex)
            {
                return Result<UserDto>.Fail(ex.Message);
            }
        }

        public async Task<Result<string?>> GetExpoPushTokenAsync(string userClerkId)
        {
            try
            {
                var spec = new UserByClerkIdSpecification(userClerkId);
                var user = await _userRepository.GetSingleAsync(spec);
                if (user == null)
                    return Result<string?>.Fail("Usuário não encontrado");
                return Result<string?>.Ok(user.ExpoPushToken);
            }
            catch (Exception ex)
            {
                return Result<string?>.Fail(ex.Message);
            }
        }

        public async Task<Result<List<UserDto>>> GetExpoTokenWithoutMe(string clerkId)
        {
            try
            {
                var spec = new UserWithExpoTokenSpecification(clerkId);
                var users = await _userRepository.ListAsync(spec);
                if (users == null || users.Count == 0)
                    return Result<List<UserDto>>.Fail("Nenhum usuário encontrado");
                var usersDto = _mapper.Map<List<UserDto>>(users);
                return Result<List<UserDto>>.Ok(usersDto);
            }
            catch (Exception ex)
            {
                return Result<List<UserDto>>.Fail(ex.Message);
            }
        }
    }
}
