using StayEase.DAL.DTO.Request;
using StayEase.DAL.DTO.Response;

namespace StayEase.BLL.Service
{
    public interface IHotelService
    {
        public List<HotelResponse> GetAll();
        public HotelResponse Create(HotelRequest request);

    }
}
