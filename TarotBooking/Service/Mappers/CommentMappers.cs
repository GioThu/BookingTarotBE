using TarotBooking.Model.CommentModel;
using TarotBooking.Models;
namespace TarotBooking.Mappers
{
    public static class CommentMappers
    {
        public static Comment ToCreateComment(this CreateCommentModel createCommentModel)
        {
            return new Comment
            {
                Id = Utils.Utils.GenerateIdModel("comment"),
                UserId = createCommentModel.UserId,
                ReaderId = createCommentModel.ReaderId,
                PostId = createCommentModel.PostId,
                Text = createCommentModel.Text,
                CreateAt = Utils.Utils.GetTimeNow()
            };
        }

        public static Comment ToUpdateComment(this UpdateCommentModel updateCommentModel)
        {
            return new Comment
            {
                Id = updateCommentModel.Id,
                Text = updateCommentModel.Text,
                CreateAt = Utils.Utils.GetTimeNow()
            };
        }
    }
}
