using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class ReaderRepo : IReaderRepo
    {
        private readonly BookingTarotContext _context;

        public ReaderRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<Reader> Add(Reader reader)
        {
            await _context.Readers.AddAsync(reader);
            await _context.SaveChangesAsync();
            return reader;
        }



        public async Task<bool> Delete(string id)
        {
            var reader = await _context.Readers.FindAsync(id);

            if (reader == null) return false;

            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Reader>> GetAll()
        {
            // Only return active readers
            return await _context.Readers
                .Where(reader => reader.Status == "Active")
                .ToListAsync();
        }

        public async Task<List<Reader>> GetAllBlocked()
        {
            return await _context.Set<Reader>().ToListAsync();
        }

        public async Task<Reader?> GetById(string id)
        {
            return await _context.Readers.FirstOrDefaultAsync(reader => reader.Id == id);
        }

        public async Task<Reader> Update(Reader reader)
        {
            var existingReader = await _context.Readers.FindAsync(reader.Id);

            if (existingReader == null)
            {
                return null;
            }

            var properties = typeof(Reader).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(reader);
                var oldValue = property.GetValue(existingReader);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingReader, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingReader;
        }

        public async Task<List<Image>> GetReaderImagesById(string readerId)
        {
            if (string.IsNullOrWhiteSpace(readerId))
            {
                throw new ArgumentException("Reader ID cannot be null or empty.", nameof(readerId));
            }

            return await _context.Images
                         .Where(image => image.ReaderId == readerId)
                         .OrderByDescending(image => image.CreateAt)
                         .ToListAsync();
        }

        public async Task<Reader> GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            return await _context.Readers.FirstOrDefaultAsync(reader => reader.Email == email && reader.Status == "Active");
        }

        public async Task<int> CountActiveReaders()
        {
            return await _context.Readers.CountAsync(reader => reader.Status == "Active");
        }


    }
}
