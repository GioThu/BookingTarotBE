using Service.Model.CardModel;
using TarotBooking.Models;

namespace TarotBooking.Mappers
{
    public static class CardMappers
    {
        

        public static Card ToUpdateCard(this UpdateCardModel updateCardModel)
        {
            return new Card
            {
                Id = updateCardModel.Id,
                GroupId = updateCardModel.GroupId,
                Element = updateCardModel.Element,
                Name = updateCardModel.Name,
                Message = updateCardModel.Message,
                CreateAt = Utils.Utils.GetTimeNow(),
            };
        }


        public static Card ToCreateCard(this CreateCardModel createCardModel)
        {
            return new Card
            {
                Id = Utils.Utils.GenerateIdModel("card"),
                GroupId = createCardModel.GroupId,
                Element = createCardModel.Element,
                Name = createCardModel.Name,
                Message = createCardModel.Message,
                CreateAt = Utils.Utils.GetTimeNow(),
                Status = "Active"
            };
        }
    }
}
