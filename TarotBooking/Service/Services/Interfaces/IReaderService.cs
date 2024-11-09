using Service.Model.FollowModel;
using Service.Model.ReaderModel;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IReaderService
    {
        Task<List<Reader>> GetAllReaders();
        Task<Reader?> UpdateReader(UpdateReaderModel updateReaderDto);
        Task<Reader?> CreateReader(CreateReaderModel createReaderModel);

        Task<bool> DeleteReader(string ReaderId);
        Task<ReaderWithImageModel?> GetReaderWithImageById(string readerId);
        Task<List<Reader>> GetPagedReadersAsync(int pageNumber, int pageSize, string searchTerm);
        Task<Reader?> ValidateReaderCredentials(string email, string password);
        Task<PagedReaderModel> GetPagedReadersInfoAsync(string? readerName, float? minPrice, float? maxPrice, List<string>? topicIds, int pageNumber = 1, int pageSize = 10);
        Task<int> CountActiveReaders();
        Task<bool> ChangePassword(ChangePasswordModel changePasswordModel);
        Task<bool> ChangeStatus(string readerId);

    }
}
