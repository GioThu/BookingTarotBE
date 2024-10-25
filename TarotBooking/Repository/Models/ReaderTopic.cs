using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class ReaderTopic
    {
        public string Id { get; set; } = null!;
        public string? ReaderId { get; set; }
        public string? TopicId { get; set; }

        public virtual Reader? Reader { get; set; }
        public virtual Topic? Topic { get; set; }
    }
}
