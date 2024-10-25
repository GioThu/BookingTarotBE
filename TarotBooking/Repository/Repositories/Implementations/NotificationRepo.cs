using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class NotificationRepo : INotificationRepo
    {
        private readonly BookingTarotContext _context;

        public NotificationRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<Notification> AddNotification(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<List<Notification>> GetNotificationsByReaderId(string readerId)
        {
            return await _context.Notifications
                .Where(n => n.ReaderId == readerId)
                .ToListAsync();
        }

        public async Task<List<Notification>> GetNotificationsByUserId(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }
    }
}
