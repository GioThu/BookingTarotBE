using TarotBooking.Model.GroupCardModel;
using TarotBooking.Models;
namespace TarotBooking.Mappers
{
    public static class GroupCardMappers
    {
        public static GroupCard ToCreateGroupCard(this CreateGroupCardModel createGroupCardModel)
        {
            return new GroupCard
            {
                Id = Utils.Utils.GenerateIdModel("groupCard"),
                Name = createGroupCardModel.Name,
                CreateAt = Utils.Utils.GetTimeNow(),
                Description = createGroupCardModel.Description,
                Status = "Active"
            };
        }

        public static GroupCard ToUpdateGroupCard(this UpdateGroupCardModel updateGroupCardModel)
        {
            return new GroupCard
            {
                Id = updateGroupCardModel.Id,
                Name = updateGroupCardModel.Name,
                CreateAt = Utils.Utils.GetTimeNow(),
                Description = updateGroupCardModel.Description
            };
        }

        public static UserGroupCard ToCreateUserGroupCard (string groupCardId, string readerId)
        {
            return new UserGroupCard
            {
                Id = Utils.Utils.GenerateIdModel("userGroupCard"),
                ReaderId = readerId,
                GroupId = groupCardId
            };
        }
    }
}
