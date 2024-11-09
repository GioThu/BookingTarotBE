using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.CommentModel
{
    public class CommentWithUserName
    {
        public string name { get; set; } = null!;
        public CommentDto Comment { get; set; } = null!;
    }
}
