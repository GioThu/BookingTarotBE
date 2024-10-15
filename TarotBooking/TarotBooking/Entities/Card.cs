using System;
using System.Collections.Generic;

namespace TarotBooking.Models
{
    public partial class Card
    {
        public Card()
        {
            CardGames = new HashSet<CardGame>();
            Images = new HashSet<Image>();
        }

        public string Id { get; set; } = null!;
        public string? GroupId { get; set; }
        public string? Element { get; set; }
        public bool? IsRightWay { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual GroupCard? Group { get; set; }
        public virtual ICollection<CardGame> CardGames { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
