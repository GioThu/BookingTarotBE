using TarotBooking.Model.ReaderTopicModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IReaderTopicService
    {
        Task<List<ReaderTopic>> GetAllReaderTopics();
        Task<ReaderTopic?> CreateReaderTopic(CreateReaderTopicModel createReaderTopicModel);
        Task<bool> DeleteReaderTopic(string readerTopicId);
        Task<List<Topic>> GetTopicsByReaderIdAsync(string readerId, int pageNumber, int pageSize);
    }
}
