using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class TopicRepo : ITopicRepo
    {
        private readonly BookingTarotContext _context;
        private const string ActiveStatus = "Active";
        public TopicRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<Topic> Add(Topic topic)
        {
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task<bool> Delete(string id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null) return false;
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Topic>> GetAll()
        {
            return await _context.Topics
                .Where(topic => topic.Status == ActiveStatus)
                .ToListAsync();
        }

        public async Task<Topic?> GetById(string id)
        {
            return await _context.Topics.FirstOrDefaultAsync(cate => cate.Id == id);
        }

        public async Task<Topic> Update(Topic topic)
        {
            var existingTopic = await _context.Topics.FindAsync(topic.Id);

            if (existingTopic == null)
            {
                return null;
            }

            var properties = typeof(Topic).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(topic);
                var oldValue = property.GetValue(existingTopic);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingTopic, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingTopic;
        }




    }
}
