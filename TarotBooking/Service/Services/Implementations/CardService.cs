using Microsoft.EntityFrameworkCore.Migrations;
using Service.Model.CardModel;
using Service.Model.ReaderModel;
using TarotBooking.Mappers;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Model.TopicModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Implementations;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Repository.Implementations;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class CardService : ICardService
    {
        private readonly ICardRepo _cardRepo;
        private readonly FirebaseService _firebaseService;


        public CardService(ICardRepo cardRepo, FirebaseService firebaseService)
        {
            _cardRepo = cardRepo;

            _firebaseService = firebaseService;
     
        }

        public async Task<Card?> CreateCard(CreateCardModel createCardDto)
        {
            var createCard = createCardDto.ToCreateCard();

            if(createCardDto.img != null)
            {
                createCard.ImageUrl = await _firebaseService.UploadImageAsync(createCardDto.img);
            }

            if (createCard == null) throw new Exception("Unable to create card!");

            return await _cardRepo.Add(createCard);
        }

        public async Task<List<Card>> CreatelistCard(List<CreateCardModel> createListCardModel)
        {
            var cards = new List<Card>();

            foreach (var createCardDto in createListCardModel)
            {
                var createdCard = await CreateCard(createCardDto);

                if (createdCard == null)
                {
                    throw new Exception("Unable to create one of the cards!");
                }

                cards.Add(createdCard);
            }

            return cards;
        }

        public async Task<bool> DeleteCard(string cardId)
        {
            var card = await _cardRepo.GetById(cardId);

            if (card == null) throw new Exception("Unable to find card!");

            return await _cardRepo.Delete(cardId);
        }

        public async Task<List<Card>> GetAllCards()
        {
            var card = await _cardRepo.GetAll();

            if (card == null) throw new Exception("No card in stock!");

            return card.ToList();
        }

        public async Task<Card?> UpdateCard(UpdateCardModel updateCardModel)
        {
            var card = await _cardRepo.GetById(updateCardModel.Id);

            if (card == null) throw new Exception("Unable to find card!");
            if (updateCardModel.Image != null)
            {
                card.ImageUrl = await _firebaseService.UploadImageAsync(updateCardModel.Image);
            }

            var updateCard = updateCardModel.ToUpdateCard();

            if (updateCard == null) throw new Exception("Unable to update card!");

            return await _cardRepo.Update(updateCard);
        }

        public async Task<List<Card>> GetListCardByGroupId(string groupId)
        {
            var allCards = await _cardRepo.GetAll(); 
            var filteredCards = allCards
                .Where(c => c.GroupId == groupId && c.Status == "Active") 
                .ToList();

            if (filteredCards == null || !filteredCards.Any())
            {
                throw new Exception("No cards found for the specified group!");
            }

            return filteredCards;
        }

    }
}
