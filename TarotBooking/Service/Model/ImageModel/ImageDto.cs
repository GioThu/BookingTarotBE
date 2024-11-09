using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.ImageModel
{
    public class ImageDto
    {
        public string Id { get; set; } = null!;
        public string? CardId { get; set; }
        public string? UserId { get; set; }
        public string? PostId { get; set; }
        public string? GroupId { get; set; }
        public string? ReaderId { get; set; }
        public string? Url { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
