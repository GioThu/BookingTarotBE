using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IUserRepo
    {
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<bool> Delete(string id);
        Task<User?> GetById(string id);
        Task<User?> GetByEmail(string email);
        Task<List<User>> GetAll();
        Task<List<Image>> GetUserImagesById(string userId);

    }
}
