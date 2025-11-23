using AutoMapper;
using CleanGo.Application.DTOs.Cleaners;
using CleanGo.Application.Interfaces;
using CleanGo.Application.Interfaces.Security;
using CleanGo.Application.Interfaces.Services;
using CleanGo.Domain.Entities;

namespace CleanGo.Application.Services.Cleaners
{
    public class CleanerService : ICleanerService
    {
        private readonly ICleanerRepository _cleanerRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public CleanerService(ICleanerRepository cleanerRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _cleanerRepository = cleanerRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<CleanerDto> CreateCleanerAsync(CreateCleanerDto createCleanerDto)
        {
            // Map CreateCleanerDto to Cleaner entity.
            var cleaner = _mapper.Map<Cleaner>(createCleanerDto);
            cleaner.Id = Guid.NewGuid();
            cleaner.PasswordHash = _passwordHasher.HashPassword(createCleanerDto.Password);
            cleaner.CreatedAt = DateTime.UtcNow;

            // Save the cleaner using the repository.
            await _cleanerRepository.AddAsync(cleaner);

            // Map Cleaner entity to CleanerDto.
            return _mapper.Map<CleanerDto>(cleaner);
        }

        public async Task<List<CleanerDto>> GetAllCleanersAsync()
        {
            var cleaners = await _cleanerRepository.GetAllAsync();

            // Map Cleaner entities to CleanerDto.
            return _mapper.Map<List<CleanerDto>>(cleaners);
        }
    }
}
