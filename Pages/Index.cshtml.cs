using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movie_ticket_booking_system.Data;
using Movie_ticket_booking_system.Models;

namespace Movie_ticket_booking_system.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;

        public List<Event> Events { get; set; } = new();

        public async Task OnGetAsync()
        {
            Events = await _db.Events
                              .AsNoTracking()
                              .OrderBy(e => e.StartsAt)
                              .ToListAsync();
        }
    }
}
