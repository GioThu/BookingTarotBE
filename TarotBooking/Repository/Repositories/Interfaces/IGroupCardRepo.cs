using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IGroupCardRepo
    {
        Task<GroupCard> Add(GroupCard groupCard);
        Task<GroupCard> Update(GroupCard groupCard);
        Task<bool> Delete(string id);
        Task<GroupCard?> GetById(string id);
        Task<List<GroupCard>> GetAll();
        Task<List<GroupCard>> GetGroupCardsByReaderIdAsync(string readerId, int pageNumber, int pageSize);
        Task<int> GetGroupCardsCountByReaderIdAsync(string readerId);
    }
}
