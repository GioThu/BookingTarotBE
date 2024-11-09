using Service.Model.TopicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.ReaderModel
{
    public class ReaderInfor
    {
        public ReaderDto? Reader { get; set; }
        public List<TopicDto>? Topics { get; set; }
        public int? CountBooking { get; set; }
        public string? Url { get; set; } 
    }
}
