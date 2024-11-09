using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.GroupCardModel
{
    public class GroupCardWithImage
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public bool? IsPublic { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? Status { get; set; }
        public string? Url { get; set; }
    }
}
