using Service.Model.FollowModel;
using Service.Model.ReaderModel;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IFollowService
    {
        Task<List<Follow>> GetAllFollows();
        Task<Follow?> CreateFollow(CreateFollowModel createFollowModel);
        Task<bool> DeleteFollow(string followId);
        Task<PagedReaderModel> GetPagedReadersWithImageByUserId(string userId, int pageNumber = 1, int pageSize = 10);
        Task<bool> DeleteFollowByUserAndReaderId(string userId, string readerId);

    }
}
