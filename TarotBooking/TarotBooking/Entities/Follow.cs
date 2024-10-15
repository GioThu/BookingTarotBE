using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class Follow
    {
        public string Id { get; set; } = null!;
        public string? FollowerId { get; set; }
        public string? ReaderId { get; set; }
        public string? Text { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual User? Follower { get; set; }
        public virtual Reader? Reader { get; set; }
    }
}
