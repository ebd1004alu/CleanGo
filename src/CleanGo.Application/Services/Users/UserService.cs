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

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            // Map CreateUserDto to User entity.
            var user = new User
            (
                createUserDto.FirstName,
                createUserDto.LastName,
                createUserDto.Phone,
                createUserDto.Email,
                createUserDto.Address,
               _passwordHasher.HashPassword(createUserDto.Password),
                DateTime.SpecifyKind(createUserDto.DateOfBirth, DateTimeKind.Utc),
                DateTime.UtcNow
            );

            // Save the user using the repository.
            await _userRepository.AddAsync(user);

            // Map User entity to UserDto.
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users =  await _userRepository.GetAllAsync();

            // Map User entities to UserDto list.
            return users.Select(User => new UserDto
            {
                Id = User.Id,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email
            }).ToList();
        }
    }
}
