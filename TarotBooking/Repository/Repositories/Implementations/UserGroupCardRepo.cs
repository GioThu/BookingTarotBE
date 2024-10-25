using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class UserGroupCardRepo : IUserGroupCardRepo
    {
        private readonly BookingTarotContext _context;

        public UserGroupCardRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<UserGroupCard> Add(UserGroupCard userGroupCard)
        {
            await _context.UserGroupCards.AddAsync(userGroupCard);
            await _context.SaveChangesAsync();
            return userGroupCard;
        }

        public async Task<bool> Delete(string id)
        {
            var userGroupCard = await _context.UserGroupCards.FindAsync(id);

            if (userGroupCard == null) return false;

            _context.UserGroupCards.Remove(userGroupCard);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserGroupCard>> GetAll()
        {
            return await _context.Set<UserGroupCard>().ToListAsync();
        }

        public async Task<UserGroupCard?> GetById(string id)
        {
            return await _context.UserGroupCards.FirstOrDefaultAsync(cate => cate.Id == id);
        }

        public async Task<UserGroupCard> Update(UserGroupCard userGroupCard)
        {
            var existingUserGroupCard = await _context.UserGroupCards.FindAsync(userGroupCard.Id);

            if (existingUserGroupCard == null)
            {
                return null;
            }

            var properties = typeof(UserGroupCard).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(userGroupCard);
                var oldValue = property.GetValue(existingUserGroupCard);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingUserGroupCard, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingUserGroupCard;
        }


    }
}
