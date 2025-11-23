using AutoMapper;
using CleanGo.Application.DTOs.Bookings;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Security;
using CleanGo.Application.Mapping;
using CleanGo.Application.Services.Bookings;
using CleanGo.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CleanGo.Tests.Services
{
    public class BookingServiceTests
    {

        private readonly IMapper _mapper;
        
        public BookingServiceTests()
        {
            // Configuration of AutoMapper.
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AppMappingProfile)); // Add AutoMapper Profile.
            var serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        [Fact]
        public async Task CreateBookingAsync_Should_Call_Repository_And_Return_BookingDto()
        {
            // Arrange.
            var bookingRepositoryMock = new Mock<IBookingRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();

            passwordHasherMock
                .Setup(ph => ph.HashPassword(It.IsAny<string>()))
                .Returns<string>(p => "hashed_" + p);

            bookingRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Booking>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Service with dependencies.
            var service = new BookingService(bookingRepositoryMock.Object, _mapper);

            var createDto = new CreateBookingDto
            {                
                CleanerId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                BookingDate = DateTime.UtcNow.AddDays(1),
                Status = "Pending"
            };

            // Act
            var result = await service.CreateBookingAsync(createDto);

            // Assert
            bookingRepositoryMock.Verify(
                r => r.AddAsync(It.Is<Booking>(b =>                    
                    b.CleanerId == createDto.CleanerId &&
                    b.ServiceId == createDto.ServiceId &&
                    Math.Abs((b.BookingDate - createDto.BookingDate).TotalSeconds) < 1 &&
                    b.Status == createDto.Status
                )),
                Times.Once
            );

            Assert.NotNull(result);            
            Assert.Equal(createDto.CleanerId, result.CleanerId);
            Assert.Equal(createDto.ServiceId, result.ServiceId);
            Assert.Equal("Pending", result.Status);
        }
    }
}
