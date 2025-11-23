using AutoMapper;
using CleanGo.Application.DTOs.Cleaners;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Security;
using CleanGo.Application.Mapping;
using CleanGo.Application.Services.Cleaners;
using CleanGo.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CleanGo.Tests.Services
{
    public class CleanerServiceTests
    {
        private readonly IMapper _mapper;

        public CleanerServiceTests()
        {
            // Configuration of AutoMapper.
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AppMappingProfile)); // Add AutoMapper Profile.
            var serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        [Fact]
        public async Task CreateCleanerAsync_Should_Call_Repository_And_Return_CleanerDto()
        {
            // Arrange.
            var cleanerRepositoryMock = new Mock<ICleanerRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();

            passwordHasherMock
                .Setup(ph => ph.HashPassword(It.IsAny<string>()))
                .Returns<string>(p => "hashed_" + p);

            cleanerRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Cleaner>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Service with dependencies.
            var service = new CleanerService(cleanerRepositoryMock.Object, passwordHasherMock.Object, _mapper);

            var cleanerService = new CleanerService(cleanerRepositoryMock.Object, passwordHasherMock.Object, _mapper);

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
            var result = await cleanerService.CreateCleanerAsync(createDto);

            // Assert.
            cleanerRepositoryMock.Verify(r => r.AddAsync(It.Is<Cleaner>(u =>
                u.Email == createDto.Email &&
                u.FirstName == createDto.FirstName &&
                u.LastName == createDto.LastName
            )), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(createDto.Email, result.Email);
            Assert.Equal(createDto.FirstName, result.FirstName);
        }
    }
}
