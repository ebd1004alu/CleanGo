using CleanGo.Application.DTOs.Users;

namespace CleanGo.Application.Interfaces.Services
{
    public interface IUserService
    {     
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    }
}
