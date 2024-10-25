using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class CardGame
    {
        public string Id { get; set; } = null!;
        public string? BookingId { get; set; }
        public string? CardId { get; set; }
        public string? Status { get; set; }
        public DateTime? Createdate { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual Card? Card { get; set; }
    }
}
