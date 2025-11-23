
using CleanGo.Domain.Entities;

namespace CleanGo.Tests.Domain
{
    public class BookingTests
    {
        public void Constructor_Should_Set_Properties_Correctly()
        {
            var bookingDate = DateTime.UtcNow.AddDays(2);           
            var booking = new Booking
               (
                new Guid(),
                new Guid(),
                new Guid(),
                bookingDate,
                "Confirmed"
                );
            Assert.Equal(bookingDate, booking.BookingDate);
            Assert.Equal("Confirmed", booking.Status);           
        }
    }
}
