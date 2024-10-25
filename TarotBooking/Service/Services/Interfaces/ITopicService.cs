using TarotBooking.Model.TopicModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface ITopicService
    {
        Task<List<Topic>> GetAllTopics();
        Task<Topic?> CreateTopic(CreateTopicModel createTopicModel);
        Task<Topic?> UpdateTopic(UpdateTopicModel updateTopicModel);
        Task<bool> DeleteTopic(string topicId);
    }
}
