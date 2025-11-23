using CleanGo.Application.DTOs.Users;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Security;
using CleanGo.Application.Mapping;
using CleanGo.Application.Services.Users;
using CleanGo.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CleanGo.Tests.Services
{
    public class UserServiceTests
    {
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            // Configuration of AutoMapper.
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AppMappingProfile)); // Add AutoMapper Profile.
            var serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        [Fact]
        public async Task CreateUserAsync_Should_Call_Repository_And_Return_UserDto()
        {
            // Arrange.
            var userRepositoryMock = new Mock<IUserRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();

            passwordHasherMock
                .Setup(ph => ph.HashPassword(It.IsAny<string>()))
                .Returns<string>(p => "hashed_" + p);

            userRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var userService = new UserService(userRepositoryMock.Object, passwordHasherMock.Object, _mapper);

            var createDto = new CreateUserDto
            {
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "000111222",
                Email = "test@example.com",
                Address = "Calle Test 1",
                Password = "secret",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // Act.
            var result = await userService.CreateUserAsync(createDto);

            // Assert.
            userRepositoryMock.Verify(r => r.AddAsync(It.Is<User>(u =>
                u.Email == createDto.Email &&
                u.FirstName == createDto.FirstName &&
                u.LastName == createDto.LastName
            )), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(createDto.Email, result.Email);
            Assert.Equal(createDto.FirstName, result.FirstName);
        }

        [Fact]
        public async Task GetAllUsersAsync_Should_Call_Repository_And_Return_ListOfUserDto()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();

            var users = new List<User>
            {
                new User("Jane", "Doe", "123456789", "jane@example.com", "Address 1", "hash", DateTime.UtcNow.AddYears(-25), DateTime.UtcNow),
                new User("John", "Smith", "987654321", "john@example.com", "Address 2", "hash", DateTime.UtcNow.AddYears(-30), DateTime.UtcNow)
            };

            userRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(users);

            var userService = new UserService(userRepositoryMock.Object, passwordHasherMock.Object, _mapper);

            // Act.
            var result = await userService.GetAllUsersAsync();

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.Email == "jane@example.com");
            Assert.Contains(result, u => u.Email == "john@example.com");
        }
    }
}
