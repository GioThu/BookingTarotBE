using Microsoft.EntityFrameworkCore;
using TarotBooking.Mappers;
using TarotBooking.Model.PostModel;
using TarotBooking.Model.PostModel;
using TarotBooking.Models;
using TarotBooking.Repositories.Interfaces;
using TarotBooking.Repository.Implementations;
using TarotBooking.Services.Interfaces;

namespace TarotBooking.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;

        public PostService(IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }


        public async Task<bool> DeletePost(string cateId)
        {
            var post = await _postRepo.GetById(cateId);

            if (post == null) throw new Exception("Unable to find post!");

            return await _postRepo.Delete(cateId);
        }

        public async Task<List<Post>> GetAllPosts()
        {
            var post = await _postRepo.GetAll();

            if (post == null) throw new Exception("No post in stock!");

            return post.ToList();
        }

        public async Task<Post?> UpdatePost(UpdatePostModel updatePostModel)
        {
            var post = await _postRepo.GetById(updatePostModel.Id);

            if (post == null) throw new Exception("Unable to find post!");

            var updatePost = updatePostModel.ToUpdatePost();

            if (updatePost == null) throw new Exception("Unable to update post!");

            return await _postRepo.Update(updatePost);
        }

        public async Task<PostWithImageModel?> GetPostWithImageById(string postId)
        {
            var post = await _postRepo.GetById(postId);
            if (post == null) throw new Exception("Post not found!");

            var images = await _postRepo.GetPostImagesById(postId);

            return new PostWithImageModel
            {
                post = post,
                url = images.Select(img => img.Url).ToList()
            };
        }

        public async Task<List<Post>> GetPagedPostsAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var posts = await _postRepo.GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                posts = posts.Where(u => u.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                             .ToList();
            }

            var pagedPosts = posts.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            return pagedPosts;
        }


    }
}
