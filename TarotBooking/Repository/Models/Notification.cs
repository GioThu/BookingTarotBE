using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class Notification
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ReaderId { get; set; }
        public string? Title { get; set; }
        public bool? IsRead { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateAt { get; set; }


        public virtual Reader? Reader { get; set; }
        public virtual User? User { get; set; }
    }
}
