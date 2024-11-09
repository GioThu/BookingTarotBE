namespace TarotBooking.Model.ReaderModel
{
    public class UpdateReaderModel
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public DateTime? Dob { get; set; }

    }
}
