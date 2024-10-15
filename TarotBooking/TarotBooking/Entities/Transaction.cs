using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class Transaction
    {
        public string Id { get; set; } = null!;
        public string? TransactionType { get; set; }
        public string? UserId { get; set; }
        public string? BookingId { get; set; }
        public string? ReaderId { get; set; }
        public double? Total { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual Reader? Reader { get; set; }
        public virtual User? User { get; set; }
    }
}
