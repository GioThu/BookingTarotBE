using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.PostModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostWebController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostWebController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [Route("posts-list")]
        public async Task<List<Post>> GetAllPosts()
        {
            return await _postService.GetAllPosts();
        }


        [HttpPost]
        [Route("update-post")]
        public async Task<Post?> UpdatePost([FromForm] UpdatePostModel updatePostModel)
        {
            return await _postService.UpdatePost(updatePostModel);
        }

        [HttpPost]
        [Route("delete-post")]
        public async Task<bool> DeletePost([FromForm] Post post)
        {
            return await _postService.DeletePost(post.Id);
        }

        [HttpGet]
        [Route("post-with-images/{postId}")]
        public async Task<ActionResult<PostWithImageModel>> GetPostWithImageById(string postId)
        {
            try
            {
                var postWithImages = await _postService.GetPostWithImageById(postId);
                if (postWithImages == null) return NotFound("Post not found.");
                return Ok(postWithImages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPagedPosts")]
        public async Task<IActionResult> GetPagedPosts(int pageNumber = 1, int pageSize = 10, string? searchTerm = "")
        {
            var posts = await _postService.GetPagedPostsAsync(pageNumber, pageSize, searchTerm);

            return Ok(posts);
        }


    }
}
