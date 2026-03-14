namespace StayEase.DAL.DTO.Response
{
    public class HotelResponse
    {
        public int Id { get; set; }
        public int StarRating { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<HotelTranslationsResponse> Translations { get; set; }
    }
}
