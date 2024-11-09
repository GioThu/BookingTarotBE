using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarotBooking.Model.PostModel;
using TarotBooking.Models;
using TarotBooking.Services.Interfaces;
using AutoMapper;
using Service.Model.PostModel;
using BE.Attributes;
using System.Data;

namespace TarotBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostWebController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostWebController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("posts-list")]
        public async Task<List<PostDto>> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts();
            return _mapper.Map<List<PostDto>>(posts);
        }

        [HttpPost]
        [Route("update-post")]
        public async Task<PostDto?> UpdatePost([FromForm] UpdatePostModel updatePostModel)
        {
            var post = await _postService.UpdatePost(updatePostModel);
            return _mapper.Map<PostDto>(post);
        }

        [HttpPost]
        [Route("delete-post")]
        public async Task<bool> DeletePost(string postId)
        {
            return await _postService.DeletePost(postId);
        }

        [HttpGet]
        [Route("post-with-images/{postId}")]
        public async Task<ActionResult<PostWithImageModel>> GetPostWithImageById(string postId)
        {
            try
            {
                var postWithImages = await _postService.GetPostWithImageById(postId);
                if (postWithImages == null) return NotFound("Post not found.");
                return postWithImages;
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

        [HttpGet("GetPagedPostsNew")]
        public async Task<IActionResult> GetPagedPostsNew(int pageNumber = 1, int pageSize = 10, string? searchTerm = "")
        {
            var posts = await _postService.GetPagedPostsNew(pageNumber, pageSize, searchTerm);
            return Ok(posts);
        }

        [HttpGet("CountReaderPosts")]
        public async Task<IActionResult> GetPostCountByReaderId(string readerId)
        {
            try
            {
                var postCount = await _postService.GetPostCountByReaderId(readerId);
                return Ok(postCount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("paged-posts-by-reader")]
        public async Task<IActionResult> GetPagedPostsByReaderId(string readerId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var pagedPosts = await _postService.GetPagedPostsByReaderIdAsync(readerId, pageNumber, pageSize);
                return Ok(pagedPosts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("post-detail/{postId}")]
        public async Task<IActionResult> GetPostDetailById(string postId)
        {
            try
            {
                var postDetail = await _postService.GetPostDetailByIdAsync(postId);
                if (postDetail == null) return NotFound("Post not found.");

                return Ok(postDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create-post")]
        public async Task<ActionResult<PostDto>> CreatePost([FromForm] CreatePostModel createPostModel)
        {
            try
            {
                var post = await _postService.CreatePost(createPostModel);

                if (post == null)
                {
                    return BadRequest("Unable to create post.");
                }

                var postDto = _mapper.Map<PostDto>(post);
                return CreatedAtAction(nameof(GetPostWithImageById), new { postId = postDto.Id }, postDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
