using TarotBooking.Model.NotificationModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface INotificationService
    {
        Task<Notification> CreateNotification(CreateNotificationModel createNotification);
        Task<List<Notification>> GetNotificationsByReaderId(string readerId);
        Task<List<Notification>> GetNotificationsByUserId(string userId);
        Task MarkNotificationAsRead(string notificationId);
        Task<(int total, int unread)> GetNotificationCountsByReaderId(string readerId);
        Task<(int total, int unread)> GetNotificationCountsByUserId(string userId);
    }
}
