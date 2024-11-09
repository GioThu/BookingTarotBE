using Service.Model.ImageModel;
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

        public async Task<Image?> FindLatestImageReaderAsync(string readerId)
        {
           
            if (string.IsNullOrEmpty(readerId))
            {
                throw new ArgumentException("Reader ID cannot be null or empty.", nameof(readerId));
            }

            var images = await _imageRepo.GetAll(); 

           
            return images
                .Where(image => image.ReaderId == readerId) 
                .OrderByDescending(image => image.CreateAt) 
                .FirstOrDefault();

        }

        public async Task<Image?> FindLatestImageUserAsync(string userId)
        {

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Reader ID cannot be null or empty.", nameof(userId));
            }

            var images = await _imageRepo.GetAll();


            return images
                .Where(image => image.UserId == userId)
                .OrderByDescending(image => image.CreateAt)
                .FirstOrDefault();

        }
        public async Task<Image?> CreateImageAndDeleteOldImages(CreateImageModel createImageDto)
        {
            var newImage = createImageDto.ToCreateImage();

            newImage.Url = await _firebaseService.UploadImageAsync(createImageDto.File);

            if (newImage == null ||
                (string.IsNullOrEmpty(newImage.ReaderId) &&
                 string.IsNullOrEmpty(newImage.UserId) &&
                 string.IsNullOrEmpty(newImage.PostId) &&
                 string.IsNullOrEmpty(newImage.GroupId) &&
                 string.IsNullOrEmpty(newImage.CardId)))
            {
                throw new Exception("Unable to create image or missing reference IDs!");
            }

            var existingImages = await _imageRepo.GetAll();

            var oldImages = existingImages.Where(image =>
                (!string.IsNullOrEmpty(newImage.ReaderId) && image.ReaderId == newImage.ReaderId) ||
                (!string.IsNullOrEmpty(newImage.UserId) && image.UserId == newImage.UserId) ||
                (!string.IsNullOrEmpty(newImage.PostId) && image.PostId == newImage.PostId) ||
                (!string.IsNullOrEmpty(newImage.GroupId) && image.GroupId == newImage.GroupId) ||
                (!string.IsNullOrEmpty(newImage.CardId) && image.CardId == newImage.CardId)).ToList();

            foreach (var oldImage in oldImages)
            {
                await _firebaseService.DeleteImageAsync(oldImage.Url);
                await _imageRepo.Delete(oldImage.Id);
            }

            return await _imageRepo.Add(newImage);
        }

        public async Task<Image?> FindImageByGroupIdAsync(string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                throw new ArgumentException("Group ID cannot be null or empty.", nameof(groupId));
            }

            var images = await _imageRepo.GetAll();

            return images
                .Where(image => image.GroupId == groupId)
                .OrderByDescending(image => image.CreateAt)
                .FirstOrDefault();
        }


    }
}