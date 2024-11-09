using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.ReaderModel
{
    public class ReaderDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public double? Rating { get; set; }
        public float? Price { get; set; }
        public string? Description { get; set; }
        public DateTime? Dob { get; set; }
        public string Status { get; set; }
    }
}
