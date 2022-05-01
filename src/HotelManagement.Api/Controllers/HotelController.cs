using HotelManagement.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHotelService _hotelService;

        public HotelController(IMediator mediator, IHotelService hotelService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _hotelService = hotelService ?? throw new ArgumentNullException(nameof(hotelService));
        }
    }
}
