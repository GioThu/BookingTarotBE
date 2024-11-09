using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class FollowRepo : IFollowRepo
    {
        private readonly BookingTarotContext _context;

        public FollowRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<Follow> Add(Follow follow)
        {
            await _context.Follows.AddAsync(follow);
            await _context.SaveChangesAsync();
            return follow;
        }

        public async Task<bool> Delete(string id)
        {
            var follow = await _context.Follows.FindAsync(id);

            if (follow == null) return false;

            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Follow>> GetAll()
        {
            return await _context.Set<Follow>().ToListAsync();
        }

        public async Task<Follow?> GetById(string id)
        {
            return await _context.Follows.FirstOrDefaultAsync(cate => cate.Id == id);
        }

        public async Task<Follow> Update(Follow follow)
        {
            var existingFollow = await _context.Follows.FindAsync(follow.Id);

            if (existingFollow == null)
            {
                return null;
            }

            var properties = typeof(Follow).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(follow);
                var oldValue = property.GetValue(existingFollow);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingFollow, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingFollow;
        }

        public async Task<List<Follow>> GetFollowsByUserId(string userId)
        {
            return await _context.Follows
                .Where(f => f.FollowerId == userId)
                .ToListAsync();
        }

        public async Task<bool> DeleteFollowByUserAndReaderId(string userId, string readerId)
        {
            var follow = await _context.Follows
                .FirstOrDefaultAsync(f => f.FollowerId == userId && f.ReaderId == readerId);

            if (follow == null) return false;

            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
