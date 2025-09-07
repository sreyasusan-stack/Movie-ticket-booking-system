using Microsoft.EntityFrameworkCore;
using Movie_ticket_booking_system.Models;

namespace Movie_ticket_booking_system.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events => Set<Event>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique booking per (Event, Seat)
            modelBuilder.Entity<Booking>()
                        .HasIndex(b => new { b.EventId, b.SeatId })
                        .IsUnique();
        }
    }
}
