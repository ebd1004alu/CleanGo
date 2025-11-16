using CleanGo.Application.Interfaces;
using CleanGo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanGo.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CleanGoDbContext _context;

        public BookingRepository(CleanGoDbContext context)
        {
            _context = context;
        }
        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
