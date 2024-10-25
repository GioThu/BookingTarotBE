using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface ITopicRepo
    {
        Task<Topic> Add(Topic topic);
        Task<Topic> Update(Topic topic);
        Task<bool> Delete(string id);
        Task<Topic?> GetById(string id);
        Task<List<Topic>> GetAll();
    }
}
