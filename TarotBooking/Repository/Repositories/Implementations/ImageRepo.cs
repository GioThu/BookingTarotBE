using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repositories.Implementations
{
    public class ImageRepo : IImageRepo
    {
        private readonly BookingTarotContext _context;

        public ImageRepo(BookingTarotContext context)
        {
            _context = context;
        }
        public async Task<Image> Add(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }
    }
}
