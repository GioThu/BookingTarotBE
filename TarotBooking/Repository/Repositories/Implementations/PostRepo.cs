using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class PostRepo : IPostRepo
    {
        private readonly BookingTarotContext _context;
        private const string ActiveStatus = "Active";
        public PostRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<Post> Add(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> Delete(string id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null) return false;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Post>> GetAll()
        {
            return await _context.Posts
                .Where(post => post.Status == ActiveStatus)
                .ToListAsync();
        }

        public async Task<Post?> GetById(string id)
        {
            return await _context.Posts.FirstOrDefaultAsync(cate => cate.Id == id);
        }

        public async Task<Post> Update(Post post)
        {
            var existingPost = await _context.Posts.FindAsync(post.Id);

            if (existingPost == null)
            {
                return null;
            }

            var properties = typeof(Post).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(post);
                var oldValue = property.GetValue(existingPost);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingPost, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingPost;
        }

        public async Task<List<Image>> GetPostImagesById(string postId)
        {
            return await _context.Images
                .Where(img => img.PostId == postId)
                .ToListAsync();
        }

        public async Task<int> GetPostCountByReaderId(string readerId)
        {
            return await _context.Posts
               .CountAsync(post => post.ReaderId == readerId && post.Status == ActiveStatus);
        }

    }
}
