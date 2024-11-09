using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.CommentModel
{
    public class CommentResponse
    {
        public List<CommentWithUserInfo> Comments { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
