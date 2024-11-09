using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class UserRepo : IUserRepo
    {
        private readonly BookingTarotContext _context;

        public UserRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users
                                 .Where(user => user.Status == "Active") 
                                 .ToListAsync();
        }

        public async Task<List<User>> GetAllBlocked()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User?> GetById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> Update(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);

            if (existingUser == null)
            {
                return null;
            }

            var properties = typeof(User).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(user);
                var oldValue = property.GetValue(existingUser);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingUser, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<List<Image>> GetUserImagesById(string userId)
        {
            return await _context.Images
                  .Where(img => img.UserId == userId)
                  .OrderByDescending(img => img.CreateAt) 
                  .ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}
