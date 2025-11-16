using CleanGo.Application.DTOs.Bookings;

namespace CleanGo.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto);
    }
}
