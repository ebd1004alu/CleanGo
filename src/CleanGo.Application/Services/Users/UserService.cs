using AutoMapper;
using CleanGo.Application.DTOs.Users;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Security;
using CleanGo.Application.Interfaces.Services;
using CleanGo.Domain.Entities;

namespace CleanGo.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            // Map CreateUserDto to User entity.
            var user = _mapper.Map<User>(createUserDto);
            user.Id = Guid.NewGuid();
            user.PasswordHash = _passwordHasher.HashPassword(createUserDto.Password);
            user.CreatedAt = DateTime.UtcNow;

            // Save the user using the repository.
            await _userRepository.AddAsync(user);

            // Map User entity to UserDto.
            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users =  await _userRepository.GetAllAsync();

            // Map User entities to UserDto list.
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
