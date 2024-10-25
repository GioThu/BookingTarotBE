using TarotBooking.Model.ReaderModel;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IReaderService
    {
        Task<List<Reader>> GetAllReaders();
        Task<Reader?> UpdateReader(UpdateReaderModel updateReaderDto);
        Task<bool> DeleteReader(string ReaderId);
        Task<ReaderWithImageModel?> GetReaderWithImageById(string readerId);
        Task<List<Reader>> GetPagedReadersAsync(int pageNumber, int pageSize, string searchTerm);

    }
}
