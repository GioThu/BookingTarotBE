using Microsoft.EntityFrameworkCore;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;

namespace TarotBooking.Repository.Implementations
{
    public class CommentRepo : ICommentRepo
    {
        private readonly BookingTarotContext _context;

        public CommentRepo(BookingTarotContext context)
        {
            _context = context;
        }

        public async Task<Comment> Add(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> Delete(string id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null) return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Comment>> GetAll()
        {
            return await _context.Set<Comment>().ToListAsync();
        }

        public async Task<Comment?> GetById(string id)
        {
            return await _context.Comments.FirstOrDefaultAsync(cate => cate.Id ==id);
        }

        public async Task<Comment> Update(Comment comment)
        {
            var existingComment = await _context.Comments.FindAsync(comment.Id);

            if (existingComment == null)
            {
                return null;
            }

            var properties = typeof(Comment).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(comment);
                var oldValue = property.GetValue(existingComment);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.SetValue(existingComment, newValue);
                }
            }

            await _context.SaveChangesAsync();
            return existingComment;
        }


        public async Task<List<Comment>> GetCommentsByPostIdAsync(string postId, int pageNumber, int pageSize)
        {
            var query = _context.Comments.AsQueryable();

            // Lọc bình luận theo postId
            query = query.Where(comment => comment.PostId == postId);

            // Phân trang
            var comments = await query.Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToListAsync();

            return comments;
        }

    }
}
