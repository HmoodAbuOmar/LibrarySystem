using StayEase.DAL.Models;

namespace StayEase.DAL.Repository
{
    public interface IHotelRepository
    {
        public List<Hotel> GetAll();
        public Hotel Create(Hotel request);
    }
}
