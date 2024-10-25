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
        private readonly IBookingRepo _bookingRepo;

        public ReaderService(IReaderRepo readerRepo, IBookingRepo bookingRepo)
        {
            _readerRepo = readerRepo;
            _bookingRepo = bookingRepo;
        }


        public async Task<bool> DeleteReader(string cateId)
        {
            var reader = await _readerRepo.GetById(cateId);

            if (reader == null) throw new Exception("Unable to find reader!");

            return await _readerRepo.Delete(cateId);
        }

        public async Task<List<Reader>> GetAllReaders()
        {
            var reader = await _readerRepo.GetAll();

            if (reader == null) throw new Exception("No reader in stock!");

            return reader.ToList();
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
                reader = reader,
                url = images.Select(img => img.Url).ToList()
            };
        }

        public async Task<List<Reader>> GetPagedReadersAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var readers = await _readerRepo.GetAll();

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


    }
}
