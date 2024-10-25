using TarotBooking.Model.ImageModel;
using TarotBooking.Models;

namespace TarotBooking.Mappers
{
    public static class ImageMappers
    {
        public static Image ToCreateImage(this CreateImageModel createImageModel)
        {
            return new Image
            {
                Id = Utils.Utils.GenerateIdModel("Image"),
                UserId = createImageModel.UserId,
                ReaderId = createImageModel.ReaderId,
                CardId = createImageModel.CardId,
                PostId = createImageModel.PostId,
                GroupId = createImageModel.GroupId,
                Url = null,
                CreateAt = Utils.Utils.GetTimeNow()
            };
        }
    }
}
