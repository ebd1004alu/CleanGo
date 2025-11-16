
namespace CleanGo.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CleanerId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = null!;

        private Booking() { } // EF Core

        public Booking(Guid userId, Guid cleanerId, Guid serviceId, DateTime bookingDate, string status)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CleanerId = cleanerId;
            ServiceId = serviceId;
            BookingDate = bookingDate;
            Status = status;
        }
    }
}
