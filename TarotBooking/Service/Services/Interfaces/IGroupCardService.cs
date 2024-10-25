using TarotBooking.Model.GroupCardModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IGroupCardService
    {
        Task<List<GroupCard>> GetAllGroupCards();
        Task<GroupCard?> CreateGroupCard(CreateGroupCardModel createGroupCardModel);
        Task<bool> DeleteGroupCard(string groupCardId);
        Task<List<GroupCard>> GetGroupCardsByReaderIdAsync(string readerId, int pageNumber, int pageSize);
    }
}
