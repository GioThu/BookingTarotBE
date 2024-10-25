using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IBookingRepo
    {
        Task<Booking> Add(Booking booking);
        Task<Booking> Update(Booking booking);
        Task<bool> Delete(string id);
        Task<Booking?> GetById(string id);
        Task<List<Booking>> GetAll();
        Task<List<Booking>> GetBookingsByReaderIdAsync(string readerId, int pageNumber, int pageSize);
        Task<List<Booking>> GetBookingsByUserIdAsync(string userId, int pageNumber, int pageSize);
    }
}
