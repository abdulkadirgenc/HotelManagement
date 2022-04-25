using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Application.Interfaces;
using System.Net;
using HotelManagement.Api.Requests;
using HotelManagement.Application.Models;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IReservationService _reservationService;

        public ReservationController(IMediator mediator, IReservationService reservationService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateReservationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreateReservationResponse>> CreateReservation(CreateReservationRequestDto request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(CancelReservationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CancelReservationResponse>> CancelReservation(CancelReservationRequestDto request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }
    }
}