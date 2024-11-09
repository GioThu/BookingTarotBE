using TarotBooking.Mappers;
using TarotBooking.Model.TopicModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepo _topicRepo;

        public TopicService(ITopicRepo topicRepo)
        {
            _topicRepo = topicRepo;
        }

        public async Task<Topic?> CreateTopic(CreateTopicModel createTopicDto)
        {
            var allTopics = await _topicRepo.GetAll();
            var duplicateActiveTopic = allTopics.Any(t => t.Name == createTopicDto.Name && t.Status == "Active");

            if (duplicateActiveTopic)
            {
                throw new Exception("A topic with this name already exists and is active.");
            }

            var createTopic = createTopicDto.ToCreateTopic();

            if (createTopic == null) throw new Exception("Unable to create topic!");

            return await _topicRepo.Add(createTopic);
        }

 

        public async Task<bool> DeleteTopic(string topicId)
        {
            var topic = await _topicRepo.GetById(topicId);

            if (topic == null) throw new Exception("Unable to find topic!");

            topic.Status = "Blocked";

            return await _topicRepo.Update(topic) != null;
        }

        public async Task<List<Topic>> GetAllTopics()
        {
            var topic = await _topicRepo.GetAll();

            if (topic == null) throw new Exception("No topic in stock!");

            return topic.ToList();
        }

        public async Task<Topic?> UpdateTopic(UpdateTopicModel updateTopicModel)
        {
            var topic = await _topicRepo.GetById(updateTopicModel.Id);

            if (topic == null) throw new Exception("Unable to find topic!");

            var updateTopic = updateTopicModel.ToUpdateTopic();

            if (updateTopic == null) throw new Exception("Unable to update topic!");

            return await _topicRepo.Update(updateTopic);
        }

    }
}
