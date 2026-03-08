namespace StayEase.DAL.DTO.Response
{
    public class HotelResponse
    {
        public int Id { get; set; }
        public List<HotelTranslationsResponse> Translations { get; set; }
    }
}
