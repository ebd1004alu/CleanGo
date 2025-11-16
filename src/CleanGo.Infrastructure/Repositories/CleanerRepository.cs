using CleanGo.Application.Interfaces;
using CleanGo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanGo.Infrastructure.Repositories
{
    public class CleanerRepository : ICleanerRepository
    {
        private readonly CleanGoDbContext _context;
        public CleanerRepository(CleanGoDbContext context)
        {
            _context = context;
        }

        public async Task<Cleaner?> GetByIdAsync(Guid id)
        {
            return await _context.Cleaners.FindAsync(id);
        }

        public async Task<IEnumerable<Cleaner>> GetAllAsync()
        {
            return await _context.Cleaners.ToListAsync();
        }

        public async Task AddAsync(Cleaner cleaner)
        {
            await _context.Cleaners.AddAsync(cleaner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cleaner cleaner)
        {
            _context.Cleaners.Update(cleaner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cleaner cleaner)
        {
            _context.Cleaners.Remove(cleaner);
            await _context.SaveChangesAsync();
        }
    }
}
