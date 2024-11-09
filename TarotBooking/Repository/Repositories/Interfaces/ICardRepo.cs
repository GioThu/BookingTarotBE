using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface ICardRepo
    {
        Task<Card> Add(Card card);
        Task<Card> Update(Card card);
        Task<bool> Delete(string id);
        Task<Card?> GetById(string id);
        Task<List<Card>> GetAll();
        Task<List<Card>> GetCardsByGroupCardId(string groupCard);
    }
}