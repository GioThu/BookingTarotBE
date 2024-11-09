using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.CardModel
{
    public class CardDto
    {
        public string Id { get; set; } = null!;
        public string? GroupId { get; set; }
        public string? Element { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? Status { get; set; }
        public string? ImageUrl { get; set; }

    }
}
