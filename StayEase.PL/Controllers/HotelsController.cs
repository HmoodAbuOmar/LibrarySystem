using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using StayEase.BLL.Service;
using StayEase.DAL.DTO.Request;
using StayEase.PL.Resources;

namespace StayEase.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IHotelService _hotelService;

        public HotelsController(IStringLocalizer<SharedResource> localizer, IHotelService hotelService)
        {
            _localizer = localizer;
            _hotelService = hotelService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var response = _hotelService.GetAll();
            return Ok(new
            {
                message = _localizer["Success"].Value,
                response
            });
        }

        [HttpPost("")]
        public IActionResult Create(HotelRequest request)
        {
            _hotelService.Create(request);
            return Ok(new { message = _localizer["Success"].Value });
        }
    }
}
