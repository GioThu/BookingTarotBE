using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Image>> GetAll()
        {
            return await _context.Images.ToListAsync(); // Retrieves all images from the database
        }

        public async Task Delete(string id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
