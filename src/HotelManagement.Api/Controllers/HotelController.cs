using HotelManagement.Api.Requests;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models.Entity;
using HotelManagement.Core.Paging;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HotelModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<HotelModel>>> GetHotels()
        {
            var hotels = await _hotelService.GetHotelList();

            return Ok(hotels);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IPagedList<HotelModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IPagedList<HotelModel>>> SearchHotels(SearchPageRequestDto request)
        {
            var hotelPagedList = await _hotelService.SearchHotels(request.Args);

            return Ok(hotelPagedList);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(HotelModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<HotelModel>> GetHotelById(GetHotelByIdRequestDto request)
        {
            var hotel = await _hotelService.GetHotelById(request.Id);

            return Ok(hotel);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<HotelModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<HotelModel>>> GetHotelsByName(GetHotelsByNameRequestDto request)
        {
            var hotels = await _hotelService.GetHotelsByName(request.Name);

            return Ok(hotels);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(HotelModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<HotelModel>> CreateHotel(CreateHotelRequestDto request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateHotel(UpdateHotelRequestDto request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> DeleteHotelById(DeleteHotelByIdRequestDto request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }
    }
}