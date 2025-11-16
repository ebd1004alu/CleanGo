using CleanGo.Application.DTOs.Services;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Services;
using CleanGo.Domain.Entities;

namespace CleanGo.Application.Services.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            // Map CreateServiceDto to Service entity.
            var service = new Service
            (
                createServiceDto.Name,
                createServiceDto.Description,
                createServiceDto.Price,
                TimeSpan.Zero
            );

            // Save the service using the repository.
            await _serviceRepository.AddAsync(service);

            // Map Service entity to ServiceDto.
            return new ServiceDto 
            { 
                Id = service.Id, 
                Name = service.Name,
                Description = service.Description,
                Price = service.Price
            };
        }

        public async Task<List<ServiceDto>> GetAllServicesAsync()
        {
            var services =  await _serviceRepository.GetAllAsync();

            // Map Service entities to ServiceDto list.
            return services.Select(service => new ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Price = service.Price
            }).ToList();
        }
    }
}
