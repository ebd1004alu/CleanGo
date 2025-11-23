using AutoMapper;
using CleanGo.Application.DTOs.Services;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Security;
using CleanGo.Application.Mapping;
using CleanGo.Application.Services.Services;
using CleanGo.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CleanGo.Tests.Services
{
    public class ServiceServiceTests
    {
        private readonly IMapper _mapper;

        public ServiceServiceTests()
        {
            // Configuration of AutoMapper.
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AppMappingProfile)); // Add AutoMapper Profile.
            var serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        [Fact]
        public async Task CreateServiceAsync_Should_Call_Repository_And_Return_ServiceDto()
        {
            // Arrange.
            var serviceRepositoryMock = new Mock<IServiceRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();

            passwordHasherMock
                .Setup(ph => ph.HashPassword(It.IsAny<string>()))
                .Returns<string>(p => "hashed_" + p);

            serviceRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Service>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Service with dependencies.
            var service = new ServiceService(serviceRepositoryMock.Object, _mapper);

            var createDto = new CreateServiceDto
            {
                Name = "Test Service",
                Description = "This is a test service",
                Price = 99.99m
            };

            // Act
            var result = await service.CreateServiceAsync(createDto);

            // Assert
            serviceRepositoryMock.Verify(
                r => r.AddAsync(It.Is<Service>(s =>
                    s.Name == createDto.Name &&
                    s.Description == createDto.Description &&
                    s.Price == createDto.Price
                )),
                Times.Once
            );

            Assert.NotNull(result);
            Assert.Equal(createDto.Name, result.Name);
            Assert.Equal(createDto.Description, result.Description);
            Assert.Equal(createDto.Price, result.Price);
        }
    }
}
