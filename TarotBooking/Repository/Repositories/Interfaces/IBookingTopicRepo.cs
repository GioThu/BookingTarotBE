using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IBookingTopicRepo
    {
        Task<BookingTopic> Add(BookingTopic bookingTopic);
        Task<BookingTopic> Update(BookingTopic bookingTopic);
        Task<bool> Delete(string id);
        Task<BookingTopic?> GetById(string id);
        Task<List<BookingTopic>> GetAll();
        Task<List<BookingTopic>> GetBookingTopicsByBookingIdAsync(string bookingId, int pageNumber, int pageSize);
    }
}
