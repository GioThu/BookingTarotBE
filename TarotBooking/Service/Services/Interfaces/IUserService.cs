using Service.Model.UserModel;
using TarotBooking.Model.TopicModel;
using TarotBooking.Model.UserModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User?> CreateUser(string fullname, string email);
        Task<User?> UpdateUser(UpdateUserModel updateUserDto);
        Task<bool> DeleteUser(string UserId);
        Task<UserWithImageModel?> GetUserWithImageById(string userId);
        Task<PagedUserModel> GetPagedUsersAsync(int pageNumber, int pageSize, string searchTerm);
        Task<User?> GetUserWithEmail(string email);
        Task<bool> ChangeStatus(string userId);
    }
}
