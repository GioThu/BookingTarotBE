using Microsoft.AspNetCore.Http;

namespace TarotBooking.Model.GroupCardModel
{
    public class CreateGroupCardModel
    {
        public string? Name { get; set; }
        public IFormFile image { get; set; }
        public string? ReaderId { get; set; }

    }
}
