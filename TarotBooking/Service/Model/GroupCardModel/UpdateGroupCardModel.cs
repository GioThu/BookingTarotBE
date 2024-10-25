using Microsoft.AspNetCore.Http;

namespace TarotBooking.Model.GroupCardModel
{
    public class UpdateGroupCardModel
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public IFormFile image {get; set;}

    }
}
