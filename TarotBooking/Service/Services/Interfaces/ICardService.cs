using Service.Model.CardModel;
using Service.Model.ReaderModel;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface ICardService
    {
        Task<List<Card>> GetAllCards();
        Task<Card?> CreateCard(CreateCardModel createCardModel);
        Task<List<Card>> CreatelistCard(List<CreateCardModel> createListCardModel);
        Task<bool> DeleteCard(string cardId);
        Task<Card?> UpdateCard(UpdateCardModel updateCardModel);
        Task<List<Card>> GetListCardByGroupId(string groupId);
    }
}
