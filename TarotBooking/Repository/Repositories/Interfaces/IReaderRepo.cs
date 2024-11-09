using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IReaderRepo
    {
        Task<List<Reader>> GetAllBlocked();
        Task<Reader> Add(Reader reader);
        Task<Reader> Update(Reader reader);
        Task<bool> Delete(string id);
        Task<Reader?> GetById(string id);
        Task<List<Reader>> GetAll();
        Task<List<Image>> GetReaderImagesById(string readerId);
        Task<Reader?> GetByEmail(string email);
        Task<int> CountActiveReaders();

    }
}
