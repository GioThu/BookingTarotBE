using TarotBooking.Model.TopicModel;
using TarotBooking.Models;

namespace TarotBooking.Mappers
{
    public static class TopicMappers
    {
        public static Topic ToCreateTopic(this CreateTopicModel createTopicModel)
        {
            return new Topic
            {
                Id = Utils.Utils.GenerateIdModel("topic"),
                Name = createTopicModel.Name
            };
        }

        public static Topic ToUpdateTopic(this UpdateTopicModel updateTopicModel)
        {
            return new Topic
            {
                Id = updateTopicModel.Id,
                Name = updateTopicModel.Name
            };
        }
    }
}
