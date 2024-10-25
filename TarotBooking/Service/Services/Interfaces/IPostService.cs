using TarotBooking.Model.PostModel;
using TarotBooking.Model.PostModel;
using TarotBooking.Models;

namespace TarotBooking.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPosts();
        Task<Post?> UpdatePost(UpdatePostModel updatePostModel);
        Task<bool> DeletePost(string PostId);
        Task<PostWithImageModel?> GetPostWithImageById(string postId);
        Task<List<Post>> GetPagedPostsAsync(int pageNumber, int pageSize, string searchTerm);
    }
}
