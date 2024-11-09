using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IImageRepo
    {
        Task<Image> Add(Image image);
        Task<List<Image>> GetAll();

        Task Delete(string id);
    }
}
