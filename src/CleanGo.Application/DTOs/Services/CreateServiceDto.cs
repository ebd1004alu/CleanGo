namespace CleanGo.Application.DTOs.Services
{
    public class CreateServiceDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
