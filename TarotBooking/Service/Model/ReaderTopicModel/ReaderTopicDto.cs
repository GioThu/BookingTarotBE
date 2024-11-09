using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.ReaderTopicModel
{
    public class ReaderTopicDto
    {
        public string Id { get; set; } = null!;
        public string? ReaderId { get; set; }
        public string? TopicId { get; set; }

    }
}
