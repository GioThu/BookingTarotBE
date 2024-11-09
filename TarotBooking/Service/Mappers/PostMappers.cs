using TarotBooking.Model.PostModel;
using TarotBooking.Models;
using TarotBooking.Utils;
namespace TarotBooking.Mappers
{
    public static class PostMappers
    {
        public static Post ToCreatePost(this CreatePostModel createPostModel)
        {
            return new Post
            {
                Id = Utils.Utils.GenerateIdModel("post"),
                ReaderId = createPostModel.ReaderId,
                Content = createPostModel.Content,
                Title = createPostModel.Title,
                Text = createPostModel.Text,
                CreateAt = Utils.Utils.GetTimeNow(),
                Status = "Active"
            };
        }

        public static Post ToUpdatePost(this UpdatePostModel updatePostModel)
        {
            return new Post
            {
                Id = updatePostModel.Id,
                Content = updatePostModel.Content,
                Title = updatePostModel.Title,
                Text = updatePostModel.Text,
                CreateAt = Utils.Utils.GetTimeNow()
            };
        }
    }
}
