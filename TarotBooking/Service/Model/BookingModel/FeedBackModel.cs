using TarotBooking.Models;

namespace TarotBooking.Model.BookingModel
{
    public class FeedbackModel
    {
        public string UserName { get; set; } = null!;
        public Booking Booking { get; set; } = null!;
    }
}
