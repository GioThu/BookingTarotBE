using Microsoft.AspNetCore.Http;

namespace TarotBooking.Model.ImageModel
{
    public class CreateImageModel
    {
        public IFormFile File { get; set; } = null!; // The image file to be uploaded
        public string? UserId { get; set; }
        public string? CardId { get; set; }
        public string? PostId { get; set; } 
        public string? GroupId { get; set; } 
        public string? ReaderId { get; set; } 
    }
}
