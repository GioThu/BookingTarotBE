using TarotBooking.Mappers;
using TarotBooking.Model.ReaderTopicModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class ReaderTopicService : IReaderTopicService
    {
        private readonly IReaderTopicRepo _readerTopicRepo;
        private readonly ITopicRepo _topicRepo;

        public ReaderTopicService(IReaderTopicRepo readerTopicRepo, ITopicRepo topicRep)
        {
            _readerTopicRepo = readerTopicRepo;
            _topicRepo = topicRep;
        }

        public async Task<ReaderTopic?> CreateReaderTopic(CreateReaderTopicModel createReaderTopicDto)
        {
            var createReaderTopic = createReaderTopicDto.ToCreateReaderTopic();

            if (createReaderTopic == null) throw new Exception("Unable to create readerTopic!");

            return await _readerTopicRepo.Add(createReaderTopic);
        }

        public async Task<bool> DeleteReaderTopic(string readerTopicId)
        {
            var readerTopic = await _readerTopicRepo.GetById(readerTopicId);

            if (readerTopic == null) throw new Exception("Unable to find readerTopic!");

            return await _readerTopicRepo.Delete(readerTopicId);
        }

        public async Task<List<ReaderTopic>> GetAllReaderTopics()
        {
            var readerTopic = await _readerTopicRepo.GetAll();

            if (readerTopic == null) throw new Exception("No readerTopic in stock!");

            return readerTopic.ToList();    
        }

        public async Task<List<Topic>> GetTopicsByReaderIdAsync(string readerId, int pageNumber, int pageSize)
        {
            var listReaderTopic = await _readerTopicRepo.GetReaderTopicsByReaderIdAsync(readerId);
            if (listReaderTopic == null || !listReaderTopic.Any()) throw new Exception("No readerTopic in stock!");

            var listTopics = new List<Topic>();

            foreach (var readerTopic in listReaderTopic)
            {
                var topic = await _topicRepo.GetById(readerTopic.TopicId);
                if (topic != null)
                {
                    listTopics.Add(topic);
                }
            }
            var paginatedTopics = listTopics
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize) 
                .ToList();

            return paginatedTopics;
        }
    }
}

