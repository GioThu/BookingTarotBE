using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class BookingTopic
    {
        public string Id { get; set; } = null!;
        public string? BookingId { get; set; }
        public string? TopicId { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual Topic? Topic { get; set; }
    }
}
