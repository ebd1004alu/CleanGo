namespace CleanGo.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }

        private Service() { } // EF Core

        public Service(string name, string description, decimal price, TimeSpan duration)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Duration = duration;
        }
    }
}
