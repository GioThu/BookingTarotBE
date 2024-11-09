using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface INotificationRepo
    {
        Task<Notification> AddNotification(Notification notification);
        Task<List<Notification>> GetNotificationsByReaderId(string readerId);
        Task<List<Notification>> GetNotificationsByUserId(string userId);
        Task<Notification?> GetById(string id);
        Task<Notification> Update(Notification notification);

    }
}
