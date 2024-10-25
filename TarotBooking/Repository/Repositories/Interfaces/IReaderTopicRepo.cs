using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IReaderTopicRepo
    {
        Task<ReaderTopic> Add(ReaderTopic readerTopic);
        Task<ReaderTopic> Update(ReaderTopic readerTopic);
        Task<bool> Delete(string id);
        Task<ReaderTopic?> GetById(string id);
        Task<List<ReaderTopic>> GetAll();
        Task<List<ReaderTopic>> GetReaderTopicsByReaderIdAsync(string readerId);
    }
}
