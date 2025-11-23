using AutoMapper;
using CleanGo.Application.DTOs.Bookings;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Services;
using CleanGo.Domain.Entities;

namespace CleanGo.Application.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            // Map CreateBookingDto to Booking entity.
            var booking = _mapper.Map<Booking>(createBookingDto);
            booking.Id = Guid.NewGuid();
            booking.BookingDate = DateTime.UtcNow;

            // Save the booking using the repository.
            await _bookingRepository.AddAsync(booking);

            // Map Booking entity to BookingDto.
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<List<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();

            // Map Booking entities to BookingDto list.
            return _mapper.Map<List<BookingDto>>(bookings);
        }
    }
}
