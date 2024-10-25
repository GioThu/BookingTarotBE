using TarotBooking.Model.ReaderModel;
using TarotBooking.Models;
using TarotBooking.Utils;
namespace TarotBooking.Mappers
{
    public static class ReaderMappers
    {

        public static Reader ToUpdateReader(this UpdateReaderModel updateReaderModel)
        {
            return new Reader
            {
                Id = updateReaderModel.Id,
                Name = updateReaderModel.Name,
                Phone = updateReaderModel.Phone,
                Password = updateReaderModel.Password,
                Rating = updateReaderModel.Rating,
                Price = (float?)Math.Round(updateReaderModel.Price, 2),
                Description = updateReaderModel.Description,
                Dob = updateReaderModel.Dob
            };
        }
    }
}
