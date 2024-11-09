using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IFollowRepo
    {
        Task<Follow> Add(Follow follow);
        Task<Follow> Update(Follow follow);
        Task<bool> Delete(string id);
        Task<Follow?> GetById(string id);
        Task<List<Follow>> GetAll();
        Task<List<Follow>> GetFollowsByUserId(string userId);
        Task<bool> DeleteFollowByUserAndReaderId(string userId, string readerId);
    }
}

