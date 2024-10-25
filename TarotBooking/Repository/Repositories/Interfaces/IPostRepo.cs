using TarotBooking.Models;

namespace TarotBooking.Repositories.Interfaces
{
    public interface IPostRepo
    {
        Task<Post> Add(Post post);
        Task<Post> Update(Post post);
        Task<bool> Delete(string id);
        Task<Post?> GetById(string id);
        Task<List<Post>> GetAll();
        Task<List<Image>> GetPostImagesById(string postId);
    }
}
