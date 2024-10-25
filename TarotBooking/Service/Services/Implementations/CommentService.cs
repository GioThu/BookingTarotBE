using TarotBooking.Mappers;
using TarotBooking.Model.CommentModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepo _commentRepo;

        public CommentService(ICommentRepo commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public async Task<Comment?> CreateComment(CreateCommentModel createCommentDto)
        {
            var createComment = createCommentDto.ToCreateComment();

            if (createComment == null) throw new Exception("Unable to create comment!");

            return await _commentRepo.Add(createComment);
        }

        public async Task<bool> DeleteComment(string commentId)
        {
            var comment = await _commentRepo.GetById(commentId);

            if (comment == null) throw new Exception("Unable to find comment!");

            return await _commentRepo.Delete(commentId);
        }

        public async Task<List<Comment>> GetAllComments()
        {
            var comment = await _commentRepo.GetAll();

            if (comment == null) throw new Exception("No comment in stock!");

            return comment.ToList();
        }

        public async Task<Comment?> UpdateComment(UpdateCommentModel updateCommentModel)
        {
            var comment = await _commentRepo.GetById(updateCommentModel.Id);

            if (comment == null) throw new Exception("Unable to find comment!");

            var updateComment = updateCommentModel.ToUpdateComment();

            if (updateComment == null) throw new Exception("Unable to update comment!");

            return await _commentRepo.Update(updateComment);
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(string postId, int pageNumber, int pageSize)
        {
            var comments = await _commentRepo.GetCommentsByPostIdAsync(postId, pageNumber, pageSize);
            return comments;
        }

    }
}
