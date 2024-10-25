using TarotBooking.Model.ReaderTopicModel;
using TarotBooking.Models;
namespace TarotBooking.Mappers
{
    public static class ReaderTopicMappers
    {
        public static ReaderTopic ToCreateReaderTopic(this CreateReaderTopicModel createReaderTopicModel)
        {
            return new ReaderTopic
            {
                Id = Utils.Utils.GenerateIdModel("readerTopic"),
                ReaderId = createReaderTopicModel.ReaderId,
                TopicId = createReaderTopicModel.TopicId
            };
        }

    }
}
