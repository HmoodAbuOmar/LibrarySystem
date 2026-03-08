using Microsoft.EntityFrameworkCore;
using StayEase.DAL.Data;
using StayEase.DAL.Models;

namespace StayEase.DAL.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Hotel> GetAll()
        {
            return _context.Hotels.Include(c => c.Translations).ToList();
        }

        public Hotel Create(Hotel request)
        {
            _context.Hotels.Add(request);
            _context.SaveChanges();
            return request;
        }



    }
}
