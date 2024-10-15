using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class Image
    {
        public string Id { get; set; } = null!;
        public string? CardId { get; set; }
        public string? UserId { get; set; }
        public string? PostId { get; set; }
        public string? GroupId { get; set; }
        public string? ReaderId { get; set; }
        public string? Url { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Card? Card { get; set; }
        public virtual GroupCard? Group { get; set; }
        public virtual Post? Post { get; set; }
        public virtual Reader? Reader { get; set; }
        public virtual User? User { get; set; }
    }
}
