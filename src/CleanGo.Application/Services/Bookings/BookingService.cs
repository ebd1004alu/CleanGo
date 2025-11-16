using CleanGo.Application.DTOs.Bookings;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Services;
using CleanGo.Domain.Entities;

namespace CleanGo.Application.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            // Map CreateBookingDto to Booking entity.
            var booking = new Booking(
                createBookingDto.UserId,
                createBookingDto.CleanerId,
                createBookingDto.ServiceId,
                createBookingDto.BookingDate,
                createBookingDto.Status

            );

            // Save the booking using the repository.
            await _bookingRepository.AddAsync(booking);

            // Map Booking entity to BookingDto.
            return new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                CleanerId = booking.CleanerId,
                ServiceId = booking.ServiceId,
                BookingDate = booking.BookingDate,
                Status = booking.Status
            };
        }

        public async Task<List<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();

            // Map Booking entities to BookingDto list.
            return bookings.Select(booking => new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                CleanerId = booking.CleanerId,
                ServiceId = booking.ServiceId,
                BookingDate = booking.BookingDate
            }).ToList();
        }
    }
}
