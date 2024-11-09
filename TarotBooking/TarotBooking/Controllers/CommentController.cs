using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.CommentModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;
using AutoMapper;
using Service.Model.CommentModel;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentWebController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentWebController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("comments-list")]
        public async Task<List<CommentDto>> GetAllComments()
        {
            var comments = await _commentService.GetAllComments();
            var result = _mapper.Map<List<CommentDto>>(comments);
            return result;
        }

        [HttpPost]
        [Route("create-comment")]
        public async Task<CommentDto?> CreateComment([FromForm] CreateCommentModel createCommentModel)
        {
            var comment = await _commentService.CreateComment(createCommentModel);
            return _mapper.Map<CommentDto>(comment);
        }

        [HttpPost]
        [Route("update-comment")]
        public async Task<CommentDto?> UpdateComment([FromForm] UpdateCommentModel updateCommentModel)
        {
            var comment = await _commentService.UpdateComment(updateCommentModel);
            return _mapper.Map<CommentDto>(comment);
        }

        [HttpPost]
        [Route("delete-comment")]
        public async Task<bool> DeleteComment(String commentId)
        {
            return await _commentService.DeleteComment(commentId);
        }

        [HttpGet("GetCommentsByPostId/{postId}")]
        public async Task<IActionResult> GetCommentsByPostId(string postId, int pageNumber = 1, int pageSize = 10)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId, pageNumber, pageSize);
            return Ok(comments);
        }
    }
}
