using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class UserGroupCard
    {
        public string Id { get; set; } = null!;
        public string? GroupId { get; set; }
        public string? ReaderId { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual GroupCard? Group { get; set; }
        public virtual Reader? Reader { get; set; }
    }
}
