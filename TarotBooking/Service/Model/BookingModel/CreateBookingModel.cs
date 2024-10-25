using TarotBooking.Models;

namespace TarotBooking.Model.BookingModel
{
    public class CreateBookingModel
    {
        public string UserId { get; set; } = null!;
        public string ReaderId { get; set; } = null!;
        public DateTime? TimeStart { get; set; } = null!;
        public DateTime? TimeEnd { get; set; } = null!;
        public List<string>? ListTopicId { get; set; }
        public string? Note { get; set; }
    }
}
