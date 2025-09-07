using System.ComponentModel.DataAnnotations;

namespace Movie_ticket_booking_system.Models
{
    public class Booking
    {
        public int Id { get; set; }

        // Foreign key
        public int SeatId { get; set; }
        public Seat Seat { get; set; } = default!;
        public int EventId { get; set; }       // ADDED
        public Event Event { get; set; } = default!;


        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; } = "";

        [Required]
        [MaxLength(15)]
        public string Phone { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}
