using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.FollowModel
{
    public class FollowDto
    {
        public string Id { get; set; } = null!;
        public string? FollowerId { get; set; }
        public string? ReaderId { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
