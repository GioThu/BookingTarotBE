using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class BookingTopicRepo : IBookingTopicRepo
    {
        private readonly BookingTarotContext _context;

        public BookingTopicRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<BookingTopic> Add(BookingTopic bookingTopic)
        {
            await _context.BookingTopics.AddAsync(bookingTopic);
            await _context.SaveChangesAsync();
            return bookingTopic;
        }

        public async Task<bool> Delete(string id)
        {
            var bookingTopic = await _context.BookingTopics.FindAsync(id);

            if (bookingTopic == null) return false;

            _context.BookingTopics.Remove(bookingTopic);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookingTopic>> GetAll()
        {
            return await _context.Set<BookingTopic>().ToListAsync();
        }

        public async Task<BookingTopic?> GetById(string id)
        {
            return await _context.BookingTopics.FirstOrDefaultAsync(cate => cate.Id == id);
        }

        public async Task<BookingTopic> Update(BookingTopic bookingTopic)
        {
            var existingBookingTopic = await _context.BookingTopics.FindAsync(bookingTopic.Id);

            if (existingBookingTopic == null)
            {
                return null;
            }

            var properties = typeof(BookingTopic).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(bookingTopic);
                var oldValue = property.GetValue(existingBookingTopic);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingBookingTopic, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingBookingTopic;
        }


        public async Task<List<BookingTopic>> GetBookingTopicsByBookingIdAsync(string bookingId, int pageNumber, int pageSize)
        {
            var query = _context.BookingTopics.AsQueryable();

            // Lọc bình luận theo postId
            query = query.Where(bookingTopic => bookingTopic.BookingId == bookingId);

            // Phân trang
            var bookingTopics = await query.Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToListAsync();

            return bookingTopics;
        }

    }
}
