using Microsoft.AspNetCore.Http;

namespace TarotBooking.Model.ReaderTopicModel
{
    public class CreateReaderTopicModel
    {
        public string? ReaderId { get; set; }
        public string? TopicId { get; set; }
    }
}
