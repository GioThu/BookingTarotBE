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
                UserId = createPostModel.UserId,
                Text = createPostModel.Text,
                CreateAt = Utils.Utils.GetTimeNow()
            };
        }

        public static Post ToUpdatePost(this UpdatePostModel updatePostModel)
        {
            return new Post
            {
                Id = updatePostModel.Id,
                Text = updatePostModel.Text,
                CreateAt = Utils.Utils.GetTimeNow()
            };
        }
    }
}
