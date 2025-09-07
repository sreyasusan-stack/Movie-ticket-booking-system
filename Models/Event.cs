using System;
using System.Collections.Generic;

namespace Movie_ticket_booking_system.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public DateTime StartsAt { get; set; }
        public string Venue { get; set; } = "";

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
