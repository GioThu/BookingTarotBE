using Service.Model.CommentModel;
using TarotBooking.Mappers;
using TarotBooking.Model.CommentModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Repository.Implementations;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IUserService _userService;
        private readonly IReaderService _readerService;

        public CommentService(ICommentRepo commentRepo, IUserService userService, IReaderService readerService)
        {
            _commentRepo = commentRepo;
            _userService = userService;
            _readerService = readerService;
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

        public async Task<CommentResponse> GetCommentsByPostIdAsync(string postId, int pageNumber, int pageSize)
        {
            var allComments = await _commentRepo.GetCommentsByPostIdAsync(postId);
            var totalCount = allComments.Count;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            // Ensure valid page number
            if (pageNumber < 1) pageNumber = 1;
            if (pageNumber > totalPages) pageNumber = totalPages;

            var commentsWithUserInfo = new List<CommentWithUserInfo>();

            // Apply pagination
            var pagedComments = allComments
                .OrderByDescending(comment => comment.CreateAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            foreach (var comment in pagedComments)
            {
                var commentWithUserInfo = new CommentWithUserInfo
                {
                    Id = comment.Id,
                    Content = comment.Text,
                    CreatedAt = comment.CreateAt,
                };

                if (!string.IsNullOrEmpty(comment.UserId))
                {
                    var user = await _userService.GetUserWithImageById(comment.UserId);
                    if (user != null)
                    {
                        commentWithUserInfo.UserName = user.user.Name;
                        commentWithUserInfo.UserImage = user.url.FirstOrDefault(); 
                    }
                }
                else if (!string.IsNullOrEmpty(comment.ReaderId))
                {
                    var reader = await _readerService.GetReaderWithImageById(comment.ReaderId);
                    if (reader != null)
                    {
                        commentWithUserInfo.UserName = reader.reader.Name;
                        commentWithUserInfo.UserImage = reader.url.FirstOrDefault();
                    }
                }

                commentsWithUserInfo.Add(commentWithUserInfo);
            }

            return new CommentResponse
            {
                Comments = commentsWithUserInfo,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }
    }


    
}
