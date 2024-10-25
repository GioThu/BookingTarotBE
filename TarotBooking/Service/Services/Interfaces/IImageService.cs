using TarotBooking.Model.CommentModel;
using TarotBooking.Model.ImageModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IImageService
    {
        Task<Image?> CreateImage(CreateImageModel createImageModel);
    }
}
