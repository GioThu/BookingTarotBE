using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class CardRepo : ICardRepo
    {
        private readonly BookingTarotContext _context;

        public CardRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<Card> Add(Card card)
        {
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<bool> Delete(string id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null) return false;

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Card>> GetAll()
        {
            return await _context.Set<Card>().ToListAsync();
        }

        public async Task<Card?> GetById(string id)
        {
            return await _context.Cards.FirstOrDefaultAsync(cate => cate.Id == id);
        }

        public async Task<Card> Update(Card card)
        {
            var existingCard = await _context.Cards.FindAsync(card.Id);

            if (existingCard == null)
            {
                return null;
            }

            var properties = typeof(Card).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(card);
                var oldValue = property.GetValue(existingCard);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingCard, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingCard;
        }

        public async Task<List<Card>> GetCardsByGroupCardId(string groupCard)
        {
            return await _context.Cards
                .Where(f => f.GroupId == groupCard)
                .ToListAsync();
        }

    }
}
