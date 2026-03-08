namespace StayEase.DAL.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;
        public Hotel Hotel { get; set; } = null!;
        public int HotelId { get; set; }
    }
}
