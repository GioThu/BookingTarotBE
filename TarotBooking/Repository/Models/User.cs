using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            Comments = new HashSet<Comment>();
            Follows = new HashSet<Follow>();
            Images = new HashSet<Image>();
            Notifications = new HashSet<Notification>();
            Posts = new HashSet<Post>();
            Transactions = new HashSet<Transaction>();
        }

        public string Id { get; set; } = null!;
        public string? RoleId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public double? Rating { get; set; }
        public int? Role { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime? Dob { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
