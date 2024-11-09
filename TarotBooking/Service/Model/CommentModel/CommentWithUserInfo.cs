using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.CommentModel
{
    public class CommentWithUserInfo
    {
        public string? Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UserName { get; set; }
        public string? UserImage { get; set; }
    }
}
