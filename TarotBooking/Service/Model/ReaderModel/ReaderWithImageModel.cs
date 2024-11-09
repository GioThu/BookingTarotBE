using Service.Model.ReaderModel;
using TarotBooking.Model.BookingModel;
using TarotBooking.Models;

namespace TarotBooking.Model.ReaderModel
{
    public class ReaderWithImageModel
    {
        public ReaderDto reader { get; set; }
        public List<string> url { get; set; }
    }
}
