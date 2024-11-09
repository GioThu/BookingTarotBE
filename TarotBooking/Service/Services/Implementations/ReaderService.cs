using AutoMapper;
using Firebase.Auth;
using Service.Model.FollowModel;
using Service.Model.ImageModel;
using Service.Model.ReaderModel;
using Service.Model.UserModel;
using System.Linq;
using TarotBooking.Mappers;
using TarotBooking.Model.ReaderModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Repository.Implementations;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepo _readerRepo;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        private readonly IReaderTopicService _readerTopicService;
        private readonly IImageService _imageService;

        public ReaderService(IReaderRepo readerRepo, IBookingService bookingService, IMapper mapper, IReaderTopicService readerTopicService, IImageService imageService)
        {
            _readerRepo = readerRepo;
            _bookingService = bookingService;
            _mapper = mapper;
            _readerTopicService = readerTopicService;
            _imageService = imageService;
        }


        public async Task<bool> DeleteReader(string cateId)
        {
            var reader = await _readerRepo.GetById(cateId);

            if (reader == null) throw new Exception("Unable to find reader!");

            return await _readerRepo.Delete(cateId);
        }

        public async Task<List<Reader>> GetAllReaders()
        {
            var reader = await _readerRepo.GetAllBlocked();

            if (reader == null) throw new Exception("No reader in stock!");

            return reader.ToList();
        }

        public async Task<Reader?> CreateReader(CreateReaderModel createReaderDto)
        {
            var createFollow = createReaderDto.ToCreateReader();

            if (createFollow == null) throw new Exception("Unable to create reader!");

            return await _readerRepo.Add(createFollow);
        }

        public async Task<Reader?> UpdateReader(UpdateReaderModel updateReaderModel)
        {
            var reader = await _readerRepo.GetById(updateReaderModel.Id);

            if (reader == null) throw new Exception("Unable to find reader!");

            var updateReader = updateReaderModel.ToUpdateReader();

            if (updateReader == null) throw new Exception("Unable to update reader!");

            return await _readerRepo.Update(updateReader);
        }

        public async Task<ReaderWithImageModel?> GetReaderWithImageById(string readerId)
        {
            var reader = await _readerRepo.GetById(readerId);
            if (reader == null) throw new Exception("Reader not found!");

            var images = await _readerRepo.GetReaderImagesById(readerId);

            return new ReaderWithImageModel
            {
                reader = _mapper.Map<ReaderDto>(reader),
                url = images.Select(img => img.Url).ToList()
            };
        }

        public async Task<List<Reader>> GetPagedReadersAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var readers = await _readerRepo.GetAll();
            readers = readers.Where(r => r.Status == "Active").ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                readers = readers.Where(u => u.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                             .ToList();
            }

            var pagedReaders = readers.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            return pagedReaders;
        }

        public async Task<Reader?> ValidateReaderCredentials(string email, string password)
        {
            var reader = await _readerRepo.GetByEmail(email);

            if (reader == null || reader.Password != password)
            {
                return null;
            }

            return reader;
        }

        public async Task<PagedReaderModel> GetPagedReadersInfoAsync(string readerName = null, float? minPrice = null, float? maxPrice = null, List<string> topicIds = null, int pageNumber = 1, int pageSize = 10)
        {
            var readerslist = await _readerRepo.GetAll();
            readerslist = readerslist.Where(r => r.Status == "Active").ToList();
            var readerInforList = new List<ReaderInfor>();

            foreach (var a in readerslist)
            {
                var topics = await _readerTopicService.GetTopicsByReaderIdAsync(a.Id);
                var countBooking = await _bookingService.GetTotalBookingCount(a.Id);
                var image = await _imageService.FindLatestImageReaderAsync(a.Id); 
                var imageUrl = image?.Url; 

                var readerInfor = new ReaderInfor
                {
                    Reader = _mapper.Map<ReaderDto>(a),
                    Topics = topics,
                    CountBooking = countBooking,
                    Url = imageUrl 
                };

                readerInforList.Add(readerInfor);
            }

            if (!string.IsNullOrEmpty(readerName))
            {
                readerInforList = readerInforList
                    .Where(readerInfo => readerInfo.Reader.Name.Contains(readerName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (minPrice.HasValue)
            {
                readerInforList = readerInforList
                    .Where(readerInfo => readerInfo.Reader.Price >= minPrice.Value)
                    .ToList();
            }

            if (maxPrice.HasValue)
            {
                readerInforList = readerInforList
                    .Where(readerInfo => readerInfo.Reader.Price <= maxPrice.Value)
                    .ToList();
            }

            if (topicIds != null && topicIds.Any())
            {
                readerInforList = readerInforList
                    .Where(readerInfo => topicIds.All(topicId => readerInfo.Topics.Any(topic => topic.Id == topicId)))
                    .ToList();
            }

            var totalItems = readerInforList.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var skip = (pageNumber - 1) * pageSize;

            var pagedReaders = readerInforList
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return new PagedReaderModel
            {
                Readers = pagedReaders,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<bool> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var reader = await _readerRepo.GetById(changePasswordModel.ReaderId);

            if (reader == null)
            {
                throw new Exception("Reader not found!");
            }

            if (reader.Password != changePasswordModel.OldPassword)
            {
                throw new UnauthorizedAccessException("Old password is incorrect.");
            }

            if (changePasswordModel.NewPassword != changePasswordModel.ConfirmPassword)
            {
                throw new Exception("New password and confirmation do not match.");
            }

            reader.Password = changePasswordModel.NewPassword;

            await _readerRepo.Update(reader);

            return true;
        }

        public async Task<int> CountActiveReaders()
        {
            return await _readerRepo.CountActiveReaders();
        }

        public async Task<bool> ChangeStatus(string readerId)
        {
            var reader = await _readerRepo.GetById(readerId);

            if (reader == null) throw new Exception("Reader not found!");

            // Toggle the status
            reader.Status = reader.Status == "Active" ? "Blocked" : "Active";

            await _readerRepo.Update(reader);
            return true;
        }
    }
}
