namespace StayEase.DAL.Models
{
    public class HotelTranslation
    {
        public int Id { get; set; }
        public string Language { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string? Address { get; set; }
        public string? Description { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public int HotelId { get; set; }
    }
}
