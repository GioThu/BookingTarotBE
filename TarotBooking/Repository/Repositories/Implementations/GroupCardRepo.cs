using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class GroupCardRepo : IGroupCardRepo
    {
        private readonly BookingTarotContext _context;

        public GroupCardRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<GroupCard> Add(GroupCard groupCard)
        {
            await _context.GroupCards.AddAsync(groupCard);
            await _context.SaveChangesAsync();
            return groupCard;
        }

        public async Task<bool> Delete(string id)
        {
            var groupCard = await _context.GroupCards.FindAsync(id);

            if (groupCard == null) return false;

            _context.GroupCards.Remove(groupCard);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GroupCard>> GetAll()
        {
            return await _context.Set<GroupCard>().ToListAsync();
        }

        public async Task<GroupCard?> GetById(string id)
        {
            return await _context.GroupCards.FirstOrDefaultAsync(cate => cate.Id == id);
        }

        public async Task<GroupCard> Update(GroupCard groupCard)
        {
            var existingGroupCard = await _context.GroupCards.FindAsync(groupCard.Id);

            if (existingGroupCard == null)
            {
                return null;
            }

            var properties = typeof(GroupCard).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(groupCard);
                var oldValue = property.GetValue(existingGroupCard);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingGroupCard, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingGroupCard;
        }


        public async Task<List<GroupCard>> GetGroupCardsByReaderIdAsync(string readerId, int pageNumber, int pageSize)
        {
            var query = _context.UserGroupCards
                                .Where(ugc => ugc.ReaderId == readerId)
                                .Select(ugc => ugc.Group) 
                                .AsQueryable();

            var groupCards = await query.Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToListAsync();

            return groupCards!;
        }

        public async Task<int> GetGroupCardsCountByReaderIdAsync(string readerId)
        {
            var count = await _context.UserGroupCards
                                       .Where(ugc => ugc.ReaderId == readerId)
                                       .Select(ugc => ugc.Group)
                                       .CountAsync();

            return count;
        }

    }
}
