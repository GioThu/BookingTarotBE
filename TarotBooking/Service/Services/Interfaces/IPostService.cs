using Service.Model.PostModel;
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
        Task<List<PostWithImageModel?>> GetPagedPostsAsync(int pageNumber, int pageSize, string searchTerm);
        Task<PagedPostModel> GetPagedPostsNew(int pageNumber, int pageSize, string searchTerm);
        Task<int> GetPostCountByReaderId(string readerId);
        Task<PagedPostModel> GetPagedPostsByReaderIdAsync(string readerId, int pageNumber, int pageSize);
        Task<PostDetail> GetPostDetailByIdAsync(string postId);
        Task<Post?> CreatePost(CreatePostModel createPostModel);
    }
}
