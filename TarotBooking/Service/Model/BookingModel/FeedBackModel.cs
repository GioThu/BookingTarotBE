using Service.Model.BookingModel;
using TarotBooking.Models;

namespace TarotBooking.Model.BookingModel
{
    public class FeedbackModel
    {
        public string UserName { get; set; } = null!;
        public BookingDto Booking { get; set; } = null!;
    }
}
