using TarotBooking.Model.BookingModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllBookings();
        Task<Booking?> CreateBooking(CreateBookingModel createBookingModel);
        Task<Booking?> CreateFeedback(CreateFeedbackModel CreateBookingModel);
        Task<bool> DeleteBooking(string bookingId);
        Task<List<FeedbackModel>> GetBookingsByReaderIdAsync(string readerId, int pageNumber, int pageSize);
        Task<List<FeedbackModel>> GetBookingsByUserIdAsync(string userId, int pageNumber, int pageSize);
    }
}
