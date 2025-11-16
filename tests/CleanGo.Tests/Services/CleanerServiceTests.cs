using CleanGo.Application.DTOs.Cleaners;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Security;
using CleanGo.Application.Services.Cleaners;
using CleanGo.Domain.Entities;
using Moq;

namespace CleanGo.Tests.Services
{
    public class CleanerServiceTests
    {
        [Fact]
        public async Task CreateCleanerAsync_Should_Call_Repository_And_Return_CleanerDto()
        {
            // Arrange.
            var cleanerRepositoryMock = new Mock<ICleanerRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();

            // Password hasher returns a hashed password.
            passwordHasherMock.Setup(ph => ph.HashPassword(It.IsAny<string>())).Returns<string>(p => "hashed_" + p);

            // Set up the user repository to add a cleaner.
            cleanerRepositoryMock.Setup(ur => ur.AddAsync(It.IsAny<Cleaner>())).Returns(Task.CompletedTask).Verifiable();

            var service = new CleanerService(cleanerRepositoryMock.Object, passwordHasherMock.Object);

            var createDto = new CreateCleanerDto
            {
                FirstName = "Test",
                LastName = "Cleaner",
                PhoneNumber = "000111222",
                Email = "test@example.com",
                Address = "Calle Test 1",
                Password = "secret",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // Act.
            var result = await service.CreateCleanerAsync(createDto);

            // Assert: verify repository was called once with a Cleaner whose Email matches.
            cleanerRepositoryMock.Verify(r => r.AddAsync(It.Is<Cleaner>(u =>
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
