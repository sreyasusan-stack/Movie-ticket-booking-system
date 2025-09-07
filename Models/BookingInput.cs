using System.ComponentModel.DataAnnotations;

namespace Movie_ticket_booking_system.Models
{
    public class BookingInput
    {
        [Required]
        [RegularExpression(@"^[A-Za-z\s]{2,50}$", ErrorMessage = "Name must contain only letters (2–50 chars).")]
        public string CustomerName { get; set; } = "";

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone must be exactly 10 digits.")]
        public string Phone { get; set; } = "";

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = "";
    }
}
