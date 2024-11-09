using TarotBooking.Model.BookingModel;
using TarotBooking.Models;
namespace TarotBooking.Mappers
{
    public static class BookingMappers
    {
        public static Booking ToCreateBooking(this CreateBookingModel createBookingModel)
        {
            return new Booking
            {
                Id = Utils.Utils.GenerateIdModel("Booking"),
                UserId = createBookingModel.UserId,
                ReaderId = createBookingModel.ReaderId,
                Status = 0,
                TimeStart = createBookingModel.TimeStart,
                TimeEnd = createBookingModel.TimeEnd,
                CreateAt = Utils.Utils.GetTimeNow(),
                Note = createBookingModel.Note
            };
        }

        public static BookingTopic ToCreateBookingTopic (string bookingId, string topicId)
        {
            return new BookingTopic
            {
                Id = Utils.Utils.GenerateIdModel("bookingTopic"),
                BookingId = bookingId,
                TopicId = topicId
            };
        }

        public static Booking ToCreateFeedback(this CreateFeedbackModel createFeedbackModel)
        {
            return new Booking
            {
                Id = createFeedbackModel.BookingId,
                Feedback = createFeedbackModel.Text,
                Rating = createFeedbackModel.Rating,
                Status = 4,
            };
        }

    }
}
