using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class Reader
    {
        public Reader()
        {
            Bookings = new HashSet<Booking>();
            Comments = new HashSet<Comment>();
            Follows = new HashSet<Follow>();
            Images = new HashSet<Image>();
            Notifications = new HashSet<Notification>();
            ReaderTopics = new HashSet<ReaderTopic>();
            Transactions = new HashSet<Transaction>();
            UserGroupCards = new HashSet<UserGroupCard>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public double? Rating { get; set; }
        public string? Description { get; set; }
        public DateTime? Dob { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<ReaderTopic> ReaderTopics { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<UserGroupCard> UserGroupCards { get; set; }
    }
}
