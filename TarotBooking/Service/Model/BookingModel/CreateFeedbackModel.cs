using TarotBooking.Models;

namespace TarotBooking.Model.BookingModel
{
    public class CreateFeedbackModel
    {
        public string BookingId { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int Rating { get; set; }
    }
}
