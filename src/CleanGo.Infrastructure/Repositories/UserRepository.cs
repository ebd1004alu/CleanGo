using CleanGo.Application.Interfaces;
using CleanGo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanGo.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CleanGoDbContext _context;

        public UserRepository(CleanGoDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync() ?? new List<User>();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
