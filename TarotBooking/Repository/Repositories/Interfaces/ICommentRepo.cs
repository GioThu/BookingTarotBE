using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface ICommentRepo
    {
        Task<Comment> Add(Comment comment);
        Task<Comment> Update(Comment comment);
        Task<bool> Delete(string id);
        Task<Comment?> GetById(string id);
        Task<List<Comment>> GetAll();
        Task<List<Comment>> GetCommentsByPostIdAsync(string postId, int pageNumber, int pageSize);
    }
}
