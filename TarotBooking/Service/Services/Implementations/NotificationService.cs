using TarotBooking.Mappers;
using TarotBooking.Model.NotificationModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepo _notificationRepo;

        public NotificationService(INotificationRepo notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        public async Task<Notification> CreateNotification(Notification notification)
        {
            return await _notificationRepo.AddNotification(notification);
        }

        public async Task<Notification> CreateNotification(CreateNotificationModel createNotificationModel)
        {
            var createNotification = createNotificationModel.ToCreateNotification();

            if (createNotification == null) throw new Exception("Unable to create notification!");
            return await _notificationRepo.AddNotification(createNotification);
        }

        public async Task<List<Notification>> GetNotificationsByReaderId(string readerId)
        {
            return await _notificationRepo.GetNotificationsByReaderId(readerId);
        }

        public async Task<List<Notification>> GetNotificationsByUserId(string userId)
        {
            return await _notificationRepo.GetNotificationsByUserId(userId);
        }

        public async Task MarkNotificationAsRead(string notificationId)
        {
            var notification = await _notificationRepo.GetById(notificationId);
            if (notification == null) throw new Exception("Notification not found");

            notification.IsRead = true;
            await _notificationRepo.Update(notification);
        }

        public async Task<(int total, int unread)> GetNotificationCountsByReaderId(string readerId)
        {
            var notifications = await _notificationRepo.GetNotificationsByReaderId(readerId);
            int total = notifications.Count;
            int unread = notifications.Count(n => n.IsRead == false);

            return (total, unread);
        }

        public async Task<(int total, int unread)> GetNotificationCountsByUserId(string userId)
        {
            var notifications = await _notificationRepo.GetNotificationsByUserId(userId);
            int total = notifications.Count;
            int unread = notifications.Count(n => n.IsRead == false);

            return (total, unread);
        }

    }
}
