using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movie_ticket_booking_system.Data;
using Movie_ticket_booking_system.Models;

namespace Movie_ticket_booking_system.Pages.Bookings
{
    public class BookModel : PageModel
    {
        private readonly AppDbContext _db;
        public BookModel(AppDbContext db) => _db = db;

        [BindProperty]
        public BookingInput Input { get; set; } = new();

        public Seat? Seat { get; set; }
        public string ErrorMessage { get; set; } = "";

        public async Task<IActionResult> OnGetAsync(int seatId)
        {
            Seat = await _db.Seats.FindAsync(seatId);
            if (Seat == null) return NotFound();
            if (Seat.IsBooked) ErrorMessage = "Sorry, this seat is already booked.";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int seatId)
        {
            if (!ModelState.IsValid)
            {
                Seat = await _db.Seats.FindAsync(seatId);
                return Page();
            }

            // Start a transaction
            using var tx = await _db.Database.BeginTransactionAsync();

            Seat = await _db.Seats
                            .Where(s => s.Id == seatId)
                            .FirstOrDefaultAsync();

            if (Seat == null)
            {
                ErrorMessage = "Seat not found.";
                return Page();
            }

            if (Seat.IsBooked)
            {
                ErrorMessage = "Sorry, this seat was just booked by someone else.";
                return Page();
            }

            try
            {
                // Mark seat as booked
                Seat.IsBooked = true;
                _db.Seats.Update(Seat);

                // Create booking record
                var booking = new Booking
                {
                    SeatId = Seat.Id,
                    CustomerName = Input.CustomerName,
                    Phone = Input.Phone,
                    Email = Input.Email
                };

                _db.Bookings.Add(booking);

                await _db.SaveChangesAsync();
                await tx.CommitAsync();

                return RedirectToPage("/Events/Seats", new { id = Seat.EventId });
            }
            catch (DbUpdateConcurrencyException)
            {
                ErrorMessage = "Oops! Another user booked this seat at the same time.";
                return Page();
            }
        }
    }
}
