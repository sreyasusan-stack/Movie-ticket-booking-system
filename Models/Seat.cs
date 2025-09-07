using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_ticket_booking_system.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string RowLabel { get; set; } = "";   // e.g., "A"
        public int Number { get; set; }              // e.g., 12
        public bool IsBooked { get; set; }

        [NotMapped]
        public string Label => $"{RowLabel}{Number}";

        // Optimistic concurrency token (SQL Server rowversion / timestamp)
        [Timestamp]
        public byte[] RowVersion { get; set; } = default!;

        public Event Event { get; set; } = default!;
    }
}
