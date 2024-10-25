namespace TarotBooking.Model.CommentModel
{
    public class CreateCommentModel
    {
        public string? UserId { get; set; }
        public string? ReaderId { get; set; }
        public string? PostId { get; set; }
        public string? Text { get; set; }
    }
}
