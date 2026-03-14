using StayEase.DAL.Models;

namespace StayEase.DAL.Repository
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetAllAsync();
        Task<Hotel> CreateAsync(Hotel request);
        Task<Hotel?> FindByIdAsync(int id);
        Task<Hotel> UpdateAsync(Hotel request);
        Task DeleteAsync(Hotel reguest);
        Task<List<Hotel>?> GetActiveHotelAsync();
    }
}
