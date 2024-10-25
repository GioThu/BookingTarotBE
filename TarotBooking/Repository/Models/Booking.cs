using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class Booking
    {
        public Booking()
        {
            BookingTopics = new HashSet<BookingTopic>();
            CardGames = new HashSet<CardGame>();
            Transactions = new HashSet<Transaction>();
        }



        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ReaderId { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public DateTime? CreateAt { get; set; }
        public double? Total { get; set; }
        public int? Rating { get; set; }
        public string? Feedback { get; set; }
        public int? Status { get; set; }
        public string? Note { get; set; }

        public virtual Reader? Reader { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<BookingTopic> BookingTopics { get; set; }
        public virtual ICollection<CardGame> CardGames { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
