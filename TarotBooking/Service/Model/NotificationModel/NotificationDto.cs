using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.NotificationModel
{
    public class NotificationDto
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ReaderId { get; set; }
        public string? Title { get; set; }
        public bool IsRead { get; set; } 
        public string? Description { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
