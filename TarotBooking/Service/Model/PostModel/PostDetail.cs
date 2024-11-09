using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarotBooking.Model.PostModel;

namespace Service.Model.PostModel
{
    public class PostDetail
    {
        public string Name { get; set; }
        public PostDto Post {get; set;}
        public List<String> Url { get; set; }
        
    }
}
