using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.PostModel
{
    public class PostDto
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ReaderId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? Status { get; set; }
    }
}
