using HotelManagement.Api.Requests;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;
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
    public class HotelRoomController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHotelRoomService _hotelRoomService;

        public HotelRoomController(IMediator mediator, IHotelRoomService hotelRoomService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _hotelRoomService = hotelRoomService ?? throw new ArgumentNullException(nameof(hotelRoomService));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HotelRoomModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<HotelRoomModel>>> GetCheapestRoomPrices()
        {
            var hotelRooms = await _hotelRoomService.GetCheapestRoomPrices();

            return Ok(hotelRooms);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<HotelModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<HotelRoomModel>>> AdvancedRoomSearch(AdvancedRoomSearchRequest request)
        {
            var filteringOptions = new List<FilteringOption>
            {
                new FilteringOption
                {
                    Field = "RoomTypeId",
                    Operator = FilteringOption.FilteringOperator.IN,
                    Value = request.RoomTypeIds
                }
            };
            var sortingOptions = new List<SortingOption>
            {
                new SortingOption
                {
                    Field = "Price",
                    Direction = SortingOption.SortingDirection.ASC,
                    Priority = 0
                }
            };

            if (request.SelectedHotelIds != null && request.SelectedHotelIds.Count > 0)
            {
                filteringOptions.Add(
                    new FilteringOption
                    {
                        Field = "HotelId",
                        Operator = FilteringOption.FilteringOperator.IN,
                        Value = request.SelectedHotelIds
                    }
                );
            }

            var pageSearchArg = new PageSearchArgs()
            {
                PageIndex = 1,
                PageSize = int.MaxValue - 1,
                PagingStrategy = PagingStrategy.NoCount,
                FilteringOptions = filteringOptions,
                SortingOptions = sortingOptions
            };

            var hotelRoomPagedList = await _hotelRoomService.AdvancedRoomSearch(pageSearchArg);

            return Ok(hotelRoomPagedList.Items);
        }
    }
}