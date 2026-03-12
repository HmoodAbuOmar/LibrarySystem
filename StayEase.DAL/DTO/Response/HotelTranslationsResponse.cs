namespace StayEase.DAL.DTO.Response
{
    public class HotelTranslationsResponse
    {
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string? Language { get; set; }
    }
}
