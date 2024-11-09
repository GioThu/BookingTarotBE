using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.BookingModel
{
    public class BookingDto
    {
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
    }
}
