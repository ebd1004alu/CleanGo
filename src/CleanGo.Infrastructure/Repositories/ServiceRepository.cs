using CleanGo.Application.Interfaces;
using CleanGo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanGo.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly CleanGoDbContext _context;
        public ServiceRepository(CleanGoDbContext context)
        {
            _context = context;
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Service service)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
}
