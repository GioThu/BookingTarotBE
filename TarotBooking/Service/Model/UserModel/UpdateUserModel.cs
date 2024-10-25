namespace TarotBooking.Model.UserModel
{
    public class UpdateUserModel
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public double? Rating { get; set; }
        public int? Role { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime? Dob { get; set; }
    }
}
