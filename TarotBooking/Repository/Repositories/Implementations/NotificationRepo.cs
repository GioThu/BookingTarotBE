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

        public async Task<Notification?> GetById(string id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(cate => cate.Id == id);
        }

        public async Task<Notification> Update(Notification notification)
        {
            var existingNotification = await _context.Notifications.FindAsync(notification.Id);

            if (existingNotification == null)
            {
                return null;
            }

            var properties = typeof(Notification).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(notification);
                var oldValue = property.GetValue(existingNotification);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingNotification, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingNotification;
        }
    }
}
