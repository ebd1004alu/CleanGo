using AutoMapper;
using CleanGo.Application.DTOs.Services;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Services;
using CleanGo.Domain.Entities;

namespace CleanGo.Application.Services.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            // Map CreateServiceDto to Service entity.
            var service = _mapper.Map<Service>(createServiceDto);
            service.Id = Guid.NewGuid();

            // Save the service using the repository.
            await _serviceRepository.AddAsync(service);

            // Map Service entity to ServiceDto.
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<List<ServiceDto>> GetAllServicesAsync()
        {
            var services =  await _serviceRepository.GetAllAsync();

            // Map Service entities to ServiceDto list.
            return _mapper.Map<List<ServiceDto>>(services);
        }
    }
}
