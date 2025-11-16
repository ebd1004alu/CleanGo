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

        public CleanerService(ICleanerRepository cleanerRepository, IPasswordHasher passwordHasher)
        {
            _cleanerRepository = cleanerRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<CleanerDto> CreateCleanerAsync(CreateCleanerDto createCleanerDto)
        {
            // Map CreateCleanerDto to Cleaner entity.
            var cleaner = new Cleaner
            (
                createCleanerDto.FirstName,
                createCleanerDto.LastName,
                createCleanerDto.PhoneNumber,
                createCleanerDto.Email,
                createCleanerDto.Address,
                _passwordHasher.HashPassword(createCleanerDto.Password),
                DateTime.SpecifyKind(createCleanerDto.DateOfBirth, DateTimeKind.Utc),
                DateTime.SpecifyKind(createCleanerDto.HireDate, DateTimeKind.Utc),
                createCleanerDto.Salary,
                DateTime.UtcNow
            );

            // Save the cleaner using the repository.
            await _cleanerRepository.AddAsync(cleaner);

            // Map Cleaner entity to CleanerDto.
            return new CleanerDto
            {
                Id = cleaner.Id,
                FirstName = cleaner.FirstName,
                LastName = cleaner.LastName,
                Email = cleaner.Email,
            };
        }

        public async Task<List<CleanerDto>> GetAllCleanersAsync()
        {
            var cleaners = await _cleanerRepository.GetAllAsync();

            // Map Cleaner entities to CleanerDto.
            return cleaners.Select(cleaner => new CleanerDto
                {
                    Id = cleaner.Id,
                    FirstName = cleaner.FirstName,
                    LastName = cleaner.LastName,
                    Email = cleaner.Email,                   
                }).ToList();
        }
    }
}
