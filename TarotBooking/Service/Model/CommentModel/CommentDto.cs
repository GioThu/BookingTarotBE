using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.CommentModel
{
    public class CommentDto
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ReaderId { get; set; }
        public string? PostId { get; set; }
        public string? Text { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
