using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarotBooking.Models;

namespace Service.Model.UserModel
{
    public class PagedUserModel
    {
        public List<UserDto> Users { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
