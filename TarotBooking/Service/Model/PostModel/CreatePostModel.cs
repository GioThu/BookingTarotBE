using Microsoft.AspNetCore.Http;

namespace TarotBooking.Model.PostModel
{
    public class CreatePostModel
    {
        public string? ReaderId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Content { get; set; }
        public IFormFile? Image  { get; set; }
    }
}
