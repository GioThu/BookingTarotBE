using TarotBooking.Model.CommentModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment?> CreateComment(CreateCommentModel createCommentModel);
        Task<Comment?> UpdateComment(UpdateCommentModel updateCommentModel);
        Task<bool> DeleteComment(string commentId);
        Task<List<Comment>> GetCommentsByPostIdAsync(string postId, int pageNumber, int pageSize);
    }
}
