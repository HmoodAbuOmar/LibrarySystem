using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using StayEase.BLL.Service;
using StayEase.DAL.DTO.Request;
using StayEase.PL.Resources;

namespace StayEase.PL.Areas.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelsController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IHotelService _hotelService;
        public HotelsController(IStringLocalizer<SharedResource> localizer, IHotelService hotelService)
        {
            _localizer = localizer;
            _hotelService = hotelService;
        }

        [HttpPost("")]
        public IActionResult Create(HotelRequest request)
        {
            _hotelService.Create(request);
            return Ok(new { message = _localizer["Success"].Value });
        }

    }
}
