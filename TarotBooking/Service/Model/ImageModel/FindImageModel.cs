using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.ImageModel
{
    public class FindImageModel
    {
        public string? UserId { get; set; }
        public string? CardId { get; set; }
        public string? PostId { get; set; }
        public string? GroupId { get; set; }
        public string? ReaderId { get; set; }
    }
}
