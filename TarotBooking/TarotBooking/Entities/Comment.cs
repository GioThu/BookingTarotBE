﻿using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class Comment
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ReaderId { get; set; }
        public string? PostId { get; set; }
        public string? Text { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Post? Post { get; set; }
        public virtual Reader? Reader { get; set; }
        public virtual User? User { get; set; }
    }
}
