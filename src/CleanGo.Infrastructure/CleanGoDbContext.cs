using Microsoft.EntityFrameworkCore;
using CleanGo.Domain.Entities;

namespace CleanGo.Infrastructure
{
    public class CleanGoDbContext : DbContext
    {
        public CleanGoDbContext(DbContextOptions<CleanGoDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }      
        public DbSet<Cleaner> Cleaners { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
