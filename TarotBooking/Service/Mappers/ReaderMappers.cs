using Service.Model.ReaderModel;
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
                Description = updateReaderModel.Description,
                Dob = updateReaderModel.Dob
            };
        }

        public static Reader ToCreateReader(this CreateReaderModel createReaderModel)
        {
            return new Reader
            {
                Id = Utils.Utils.GenerateIdModel("reader"),
                Name = createReaderModel.Name,
                Password = createReaderModel.Password,
                Email = createReaderModel.Email,
                Status = "Active"
            };
        }
    }
}
