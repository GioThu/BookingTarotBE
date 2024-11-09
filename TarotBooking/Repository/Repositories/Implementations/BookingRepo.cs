using Microsoft.EntityFrameworkCore;
using System.Linq;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class BookingRepo : IBookingRepo
    {
        private readonly BookingTarotContext _context;

        public BookingRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<Booking> Add(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> Delete(string id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Booking>> GetAll()
        {
            return await _context.Bookings
                .Where(b => b.Status != 5) 
                .ToListAsync();
        }

        public async Task<Booking?> GetById(string id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(cate => cate.Id == id);
        }

        public async Task<Booking> Update(Booking booking)
        {
            var existingBooking = await _context.Bookings.FindAsync(booking.Id);

            if (existingBooking == null)
            {
                return null;
            }

            var properties = typeof(Booking).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(booking);
                var oldValue = property.GetValue(existingBooking);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingBooking, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingBooking;
        }


        public async Task<List<Booking>> GetBookingsByReaderIdAsync(string readerId, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(readerId))
                throw new ArgumentException("ReaderId cannot be null or empty", nameof(readerId));

            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Page number and page size must be greater than zero.");

            return await _context.Bookings
                .Where(b => b.ReaderId == readerId && b.Status != 5)   
                .OrderBy(b => b.TimeStart)            
                .Skip((pageNumber - 1) * pageSize)  
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByUserIdAsync(string userId, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("UserId cannot be null or empty", nameof(userId));

            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Page number and page size must be greater than zero.");

            return await _context.Bookings
                .Where(b => b.UserId == userId && b.Status != 5)       
                .OrderBy(b => b.TimeStart)           
                .Skip((pageNumber - 1) * pageSize)    
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetBookingCountToday(string readerId)
        {
            return await _context.Bookings
                .CountAsync(b => b.ReaderId == readerId && b.TimeStart.Value.Date == DateTime.UtcNow.Date);
        }

        public async Task<int> GetTotalBookingCount(string readerId)
        {
            return await _context.Bookings
                .CountAsync(b => b.ReaderId == readerId);
        }

        public async Task<List<Booking>> GetBookingsByReaderId(string readerId, List<int>? statuses, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Bookings.AsQueryable();

            // Lọc theo readerId
            query = query.Where(b => b.ReaderId == readerId);

            // Lọc theo status (nếu có)
            if (statuses != null && statuses.Any())
            {
                query = query.Where(b => statuses.Contains((int)b.Status)); 
            }

            // Lọc theo ngày
            if (startDate.HasValue)
            {
                query = query.Where(b => b.TimeStart >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(b => b.TimeStart <= endDate.Value);
            }

            return await query.ToListAsync();
        }

    }
}

