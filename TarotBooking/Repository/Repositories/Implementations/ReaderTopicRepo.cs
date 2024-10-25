using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class ReaderTopicRepo : IReaderTopicRepo
    {
        private readonly BookingTarotContext _context;

        public ReaderTopicRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<ReaderTopic> Add(ReaderTopic readerTopic)
        {
            await _context.ReaderTopics.AddAsync(readerTopic);
            await _context.SaveChangesAsync();
            return readerTopic;
        }

        public async Task<bool> Delete(string id)
        {
            var readerTopic = await _context.ReaderTopics.FindAsync(id);

            if (readerTopic == null) return false;

            _context.ReaderTopics.Remove(readerTopic);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReaderTopic>> GetAll()
        {
            return await _context.Set<ReaderTopic>().ToListAsync();
        }

        public async Task<ReaderTopic?> GetById(string id)
        {
            return await _context.ReaderTopics.FirstOrDefaultAsync(cate => cate.Id == id);
        }

        public async Task<ReaderTopic> Update(ReaderTopic readerTopic)
        {
            var existingReaderTopic = await _context.ReaderTopics.FindAsync(readerTopic.Id);

            if (existingReaderTopic == null)
            {
                return null;
            }

            var properties = typeof(ReaderTopic).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(readerTopic);
                var oldValue = property.GetValue(existingReaderTopic);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingReaderTopic, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingReaderTopic;
        }


        public async Task<List<ReaderTopic>> GetReaderTopicsByReaderIdAsync(string readerId)
        {
            var query = _context.ReaderTopics
                                .Where(ugc => ugc.ReaderId == readerId)
                                .AsQueryable();

            var readerTopics = await query.ToListAsync();
            return readerTopics;
        }
    }
}
