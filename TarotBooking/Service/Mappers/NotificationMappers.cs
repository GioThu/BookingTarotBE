using TarotBooking.Model.NotificationModel;
using TarotBooking.Models;

namespace TarotBooking.Mappers
{
    public static class NotificationMappers
    {
        public static Notification ToCreateNotification(this CreateNotificationModel createNotificationModel)
        {
            return new Notification
            {
                Id = Utils.Utils.GenerateIdModel("notification"),
                UserId = createNotificationModel.UserId,
                ReaderId = createNotificationModel.ReaderId,
                Description = createNotificationModel.Text,
                IsRead = false,
                CreateAt = Utils.Utils.GetTimeNow()
            };
        }
    }
}
