using CleanGo.Application.DTOs.Services;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Services.Services;
using CleanGo.Domain.Entities;
using Moq;

namespace CleanGo.Tests.Services
{
    public class ServiceServiceTests
    {
        [Fact]
        public async Task CreateServiceAsync_Should_Call_Repository_And_Return_ServiceDto()
        {
            // Arrange
            var serviceRepositoryMock = new Mock<IServiceRepository>();

            serviceRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Service>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new ServiceService(serviceRepositoryMock.Object);

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
