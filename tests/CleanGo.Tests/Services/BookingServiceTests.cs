using CleanGo.Application.DTOs.Bookings;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Services.Bookings;
using CleanGo.Domain.Entities;
using Moq;

namespace CleanGo.Tests.Services
{
    public class BookingServiceTests
    {
        [Fact]
        public async Task CreateBookingAsync_Should_Call_Repository_And_Return_BookingDto()
        {
            // Arrange
            var bookingRepositoryMock = new Mock<IBookingRepository>();

            bookingRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Booking>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new BookingService(bookingRepositoryMock.Object);

            var createDto = new CreateBookingDto
            {
                UserId = Guid.NewGuid(),
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
                    b.UserId == createDto.UserId &&
                    b.CleanerId == createDto.CleanerId &&
                    b.ServiceId == createDto.ServiceId &&
                    Math.Abs((b.BookingDate - createDto.BookingDate).TotalSeconds) < 1 &&
                    b.Status == createDto.Status
                )),
                Times.Once
            );

            Assert.NotNull(result);
            Assert.Equal(createDto.UserId, result.UserId);
            Assert.Equal(createDto.CleanerId, result.CleanerId);
            Assert.Equal(createDto.ServiceId, result.ServiceId);
            Assert.Equal("Pending", result.Status);
        }
    }
}
