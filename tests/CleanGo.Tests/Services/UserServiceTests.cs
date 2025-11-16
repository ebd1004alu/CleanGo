using CleanGo.Application.DTOs.Users;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Security;
using CleanGo.Application.Services.Users;
using CleanGo.Domain.Entities;
using Moq;

namespace CleanGo.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUserAsync_Should_Call_Repository_And_Return_UserDto()
        {
            // Arrange.
            var userRepositoryMock = new Mock<IUserRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();

            // Password hasher returns a hashed password.
            passwordHasherMock.Setup(ph => ph.HashPassword(It.IsAny<string>())).Returns<string>(p => "hashed_" + p);

            // Set up the user repository to add a user.
            userRepositoryMock.Setup(ur => ur.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask).Verifiable();

            var service = new UserService(userRepositoryMock.Object, passwordHasherMock.Object);

            var createDto = new CreateUserDto
            {
                FirstName = "Test",
                LastName = "User",
                Phone = "000111222",
                Email = "test@example.com",
                Address = "Calle Test 1",
                Password = "secret",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // Act.
            var result = await service.CreateUserAsync(createDto);

            // Assert: verify repository was called once with a User whose Email matches.
            userRepositoryMock.Verify(r => r.AddAsync(It.Is<User>(u =>
                u.Email == createDto.Email &&
                u.FirstName == createDto.FirstName &&
                u.LastName == createDto.LastName
                )), 
                Times.Once
            );

            Assert.NotNull(result);
            Assert.Equal(createDto.Email, result.Email);
            Assert.Equal($"{createDto.FirstName}", result.FirstName);
        }
    }
}
