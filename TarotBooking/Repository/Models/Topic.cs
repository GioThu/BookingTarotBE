using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class Topic
    {
        public Topic()
        {
            BookingTopics = new HashSet<BookingTopic>();
            ReaderTopics = new HashSet<ReaderTopic>();
        }
        
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<BookingTopic> BookingTopics { get; set; }
        public virtual ICollection<ReaderTopic> ReaderTopics { get; set; }
    }
}
