using Service.Model.FollowModel;
using TarotBooking.Models;

namespace TarotBooking.Mappers
{
    public static class FollowMappers
    {
        public static Follow ToCreateFollow(this CreateFollowModel createFollowModel)
        {
            return new Follow
            {
                Id = Utils.Utils.GenerateIdModel("follow"),
                FollowerId = createFollowModel.FollowerId,
                ReaderId = createFollowModel.ReaderId,
                CreateAt = Utils.Utils.GetTimeNow()
            };
        }

    }
}
