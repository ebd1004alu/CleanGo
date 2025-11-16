namespace CleanGo.Application.DTOs.Bookings
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CleanerId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
