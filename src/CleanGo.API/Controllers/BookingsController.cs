using Microsoft.AspNetCore.Mvc;
using CleanGo.Application.DTOs.Bookings;
using CleanGo.Application.Interfaces.Services;

namespace CleanGo.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto createBookingDto)
        {
            var booking = await _bookingService.CreateBookingAsync(createBookingDto);
            return CreatedAtAction(nameof(GetAllBookings), new { id = booking.Id }, booking);
        }
    }
}
