using CleanGo.Application.DTOs.Services;
using CleanGo.Domain.Entities;

namespace CleanGo.Application.Interfaces.Services
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetAllServicesAsync();
        Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto);
    }
}
