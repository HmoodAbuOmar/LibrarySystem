namespace StayEase.DAL.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int MaxCapacity { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Room> Rooms { get; set; }
    }
}
