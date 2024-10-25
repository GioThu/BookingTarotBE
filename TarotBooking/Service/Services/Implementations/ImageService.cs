using TarotBooking.Mappers;
using TarotBooking.Model.ImageModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Services.Interfaces;
namespace TarotBooking.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IImageRepo _imageRepo;
        private readonly FirebaseService _firebaseService;

        public ImageService(IImageRepo imageRepo, FirebaseService firebaseService)
        {
            _imageRepo = imageRepo;
            _firebaseService = firebaseService;
        }

        public async Task<Image?> CreateImage(CreateImageModel createImageDto)
        {
            var createImage = createImageDto.ToCreateImage();
            createImage.Url = await _firebaseService.UploadImageAsync(createImageDto.File);
            if (createImage == null) throw new Exception("Unable to create image!");

            return await _imageRepo.Add(createImage);
        }
    }
}
