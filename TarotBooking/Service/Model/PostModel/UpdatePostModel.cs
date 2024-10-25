namespace TarotBooking.Model.PostModel
{
    public class UpdatePostModel
    {
        public string Id { get; set; } = null!;
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Status { get; set; }
    }
}
