using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movie_ticket_booking_system.Data;
using Movie_ticket_booking_system.Models;

namespace Movie_ticket_booking_system.Pages.Events
{
    

    public class SeatsModel : PageModel
    {
        private readonly AppDbContext _db;
        public SeatsModel(AppDbContext db) => _db = db;

        public Event ? Event { get; set; } 
        public List<Seat> Seats { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Event = await _db.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (Event == null) return NotFound();

            Seats = await _db.Seats
                             .Where(s => s.EventId == id)
                             .OrderBy(s => s.RowLabel)
                             .ThenBy(s => s.Number)
                             .ToListAsync();

            return Page();
        }
    }
}
