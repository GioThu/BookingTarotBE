using Service.Model.FollowModel;
using Service.Model.ReaderModel;
using TarotBooking.Mappers;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Repository.Implementations;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepo _followRepo;
        private readonly IReaderService _readerService;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly IReaderTopicService _readerTopicService;
        private readonly IBookingService _bookingService;


        public FollowService(IFollowRepo followRepo, IReaderService readerService, IUserService userService, IImageService imageService, IReaderTopicService readerTopicService, IBookingService bookingService)
        {
            _followRepo = followRepo;
            _readerService = readerService;  
            _userService = userService;
            _imageService = imageService;
            _readerTopicService = readerTopicService;
            _bookingService = bookingService;
        }

        public async Task<Follow?> CreateFollow(CreateFollowModel createFollowDto)
        {
            var createFollow = createFollowDto.ToCreateFollow();

            if (createFollow == null) throw new Exception("Unable to create follow!");

            return await _followRepo.Add(createFollow);
        }

        public async Task<bool> DeleteFollow(string followId)
        {
            var follow = await _followRepo.GetById(followId);

            if (follow == null) throw new Exception("Unable to find follow!");

            return await _followRepo.Delete(followId);
        }

        public async Task<List<Follow>> GetAllFollows()
        {
            var follow = await _followRepo.GetAll();

            if (follow == null) throw new Exception("No follow in stock!");

            return follow.ToList();
        }

        public async Task<PagedReaderModel> GetPagedReadersWithImageByUserId(string userId, int pageNumber = 1, int pageSize = 10)
        {
            var follows = await _followRepo.GetFollowsByUserId(userId);

            if (follows == null || !follows.Any())
                throw new Exception("No follows found for the user!");

            var readerIds = follows.Select(f => f.ReaderId).Distinct().ToList();
            var totalItems = readerIds.Count;

            // Fetch readers with images for the current page
            var pagedReaderIds = readerIds.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var readersWithImage = new List<ReaderInfor>();

            foreach (var readerId in pagedReaderIds)
            {
                var reader = await _readerService.GetReaderWithImageById(readerId);
                var topics = await _readerTopicService.GetTopicsByReaderIdAsync(readerId);
                var image = await _imageService.FindLatestImageReaderAsync(readerId);
                var countBooking = await _bookingService.GetTotalBookingCount(readerId);
                if (reader != null)
                {
                    readersWithImage.Add(new ReaderInfor
                    {
                        Reader = reader.reader,
                        Url = image?.Url,
                        Topics = topics,
                        CountBooking = countBooking
                    });
                }
            }

            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return new PagedReaderModel
            {
                Readers = readersWithImage,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<bool> DeleteFollowByUserAndReaderId(string userId, string readerId)
        {
            return await _followRepo.DeleteFollowByUserAndReaderId(userId, readerId);
        }

    }
}
