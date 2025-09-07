
using Movie_ticket_booking_system.Models;
using Microsoft.EntityFrameworkCore;

namespace Movie_ticket_booking_system.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext db)
        {
            // If we already have events, no need to seed again
            if (await db.Events.AnyAsync())
                return;

            var ev = new Event
            {
                Title = "Premiere Night",
                Venue = "Grand Cinema",
                StartsAt = DateTime.Today.AddDays(1).AddHours(19)
            };

            db.Events.Add(ev);
            await db.SaveChangesAsync();

            // Generate seats: Rows A–E, Numbers 1–10 (50 seats total)
            var rows = Enumerable.Range(0, 5).Select(i => ((char)('A' + i)).ToString());
            foreach (var r in rows)
            {
                for (int n = 1; n <= 10; n++)
                {
                    db.Seats.Add(new Seat
                    {
                        EventId = ev.Id,
                        RowLabel = r,
                        Number = n,
                        IsBooked = false
                    });
                }
            }

            await db.SaveChangesAsync();
        }
    }
}
