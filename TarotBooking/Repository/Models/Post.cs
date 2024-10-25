using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Images = new HashSet<Image>();
        }

        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? Text { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? Status { get; set; }
        public string? Title { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
