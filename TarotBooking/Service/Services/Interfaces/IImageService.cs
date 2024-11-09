using Service.Model.ImageModel;
using TarotBooking.Model.CommentModel;
using TarotBooking.Model.ImageModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IImageService
    {
        Task<Image?> CreateImage(CreateImageModel createImageModel);
        Task<Image?> FindLatestImageReaderAsync(string readerId);
        Task<Image?> FindLatestImageUserAsync(string userId);
        Task<Image?> CreateImageAndDeleteOldImages(CreateImageModel createImageDto);
        Task<Image?> FindImageByGroupIdAsync(string groupId);
    }
}
