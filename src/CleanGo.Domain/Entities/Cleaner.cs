namespace CleanGo.Domain.Entities
{
    public class Cleaner
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Booking>? Bookings { get; set; }

        private Cleaner() { } // EF Core

        public Cleaner(Guid id, string firstName, string lastName, string phoneNumber, string email, string address, string passwordHash, DateTime dateOfBirth, DateTime hireDate, decimal salary, DateTime createdAt)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            PasswordHash = passwordHash;
            DateOfBirth = dateOfBirth;
            HireDate = hireDate;
            Salary = salary;
            CreatedAt = createdAt;
        }
    }
}
