using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class GroupCard
    {
        public GroupCard()
        {
            Cards = new HashSet<Card>();
            Images = new HashSet<Image>();
            UserGroupCards = new HashSet<UserGroupCard>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public bool? IsPublic { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<UserGroupCard> UserGroupCards { get; set; }
    }
}
