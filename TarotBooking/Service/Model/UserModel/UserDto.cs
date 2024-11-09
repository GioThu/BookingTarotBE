using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.UserModel
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string? RoleId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public double? Rating { get; set; }
        public int? Role { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime? Dob { get; set; }

    }
}
