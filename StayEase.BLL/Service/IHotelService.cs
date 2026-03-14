using StayEase.DAL.DTO.Request;
using StayEase.DAL.DTO.Response;

namespace StayEase.BLL.Service
{
    public interface IHotelService
    {
        Task<List<HotelResponse>> GetAll();
        Task<HotelResponse> GetById(int id);
        Task<HotelResponse> Create(HotelRequest request);
        Task<BaseResponse> UpdateHotelAsync(int id, HotelRequest request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<List<HotelResponse>> GetActiveHotelAsync();

    }
}
