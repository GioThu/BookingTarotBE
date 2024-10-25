using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IUserGroupCardRepo
    {
        Task<UserGroupCard> Add(UserGroupCard userGroupCard);
        Task<UserGroupCard> Update(UserGroupCard userGroupCard);
        Task<bool> Delete(string id);
        Task<UserGroupCard?> GetById(string id);
        Task<List<UserGroupCard>> GetAll();
    }
}
