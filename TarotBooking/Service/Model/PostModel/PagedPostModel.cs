using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarotBooking.Model.PostModel;

namespace Service.Model.PostModel
{
    public class PagedPostModel
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<PostWithImageModel> Posts { get; set; }
    }
}
