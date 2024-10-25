using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IReaderRepo
    {
        Task<Reader> Add(Reader reader);
        Task<Reader> Update(Reader reader);
        Task<bool> Delete(string id);
        Task<Reader?> GetById(string id);
        Task<List<Reader>> GetAll();
        Task<List<Image>> GetReaderImagesById(string readerId);
    }
}
