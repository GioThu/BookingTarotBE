using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.CommentModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentWebController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentWebController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("comments-list")]
        public async Task<List<Comment>> GetAllComments()
        {
            return await _commentService.GetAllComments();
        }


        [HttpPost]
        [Route("create-comment")]
        public async Task<Comment?> CreateComment([FromForm] CreateCommentModel createCommentModel)
        {
            return await _commentService.CreateComment(createCommentModel);
        }

        [HttpPost]
        [Route("update-comment")]
        public async Task<Comment?> UpdateComment([FromForm] UpdateCommentModel updateCommentModel)
        {
            return await _commentService.UpdateComment(updateCommentModel);
        }

        [HttpPost]
        [Route("delete-comment")]
        public async Task<bool> DeleteComment([FromForm] Comment comment)
        {
            return await _commentService.DeleteComment(comment.Id);
        }


        [HttpGet("GetCommentsByPostId/{postId}")]
        public async Task<IActionResult> GetCommentsByPostId(string postId, int pageNumber = 1, int pageSize = 10)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId, pageNumber, pageSize);
            return Ok(comments);
        }
    }
}
