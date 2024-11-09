using Service.Model.BookingModel;
using TarotBooking.Model.BookingModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllBookings();
        Task<Booking> GetBookingById(string bookingId);
        Task<Booking?> CreateBooking(CreateBookingModel createBookingModel);
        Task<Booking?> CreateFeedback(CreateFeedbackModel CreateBookingModel);
        Task<bool> DeleteBooking(string bookingId);
        Task<List<FeedbackModel>> GetBookingsByReaderIdAsync(string readerId, int pageNumber, int pageSize);
        Task<List<FeedbackModel>> GetBookingsByUserIdAsync(string userId, int pageNumber, int pageSize);
        Task<int> GetBookingCountToday(string readerId);
        Task<int> GetTotalBookingCount(string readerId);
        Task<PagedBookingResult> GetPagedBookings(string readerId, int pageNumber, int pageSize, string? searchTerm = null, List<int>? statuses = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<FeedbackModel>> GetBookingsWithFeedback(string readerId);
        Task<List<TimeSlot>> GetAvailableTimeSlots(DateTime date, string readerId);
        Task<PagedBookingResult> GetPagedBookingsByDay(string readerId, int pageNumber, int pageSize);
        Task<Booking> ChangeBookingStatus(string bookingId, int status);

    }
}
