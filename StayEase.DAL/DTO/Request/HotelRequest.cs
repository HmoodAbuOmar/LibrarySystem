namespace StayEase.DAL.DTO.Request
{
    public class HotelRequest
    {
        public int StarRating { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<HotelTranslationsRequest> Translations { get; set; }
    }
}
