namespace StayEase.DAL.DTO.Request
{
    public class HotelTranslationsRequest
    {
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string? Language { get; set; }
    }
}
