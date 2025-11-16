using CleanGo.Application.DTOs.Cleaners;

namespace CleanGo.Application.Interfaces.Services
{
    public interface ICleanerService
    {
        Task<List<CleanerDto>> GetAllCleanersAsync();
        Task<CleanerDto> CreateCleanerAsync(CreateCleanerDto createCleanerDto);
    }
}
