using HotelManagement.Api.Requests;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;
using MediatR;

namespace HotelManagement.Api.Application.Commands
{
    public class CancelReservationCommandHandler : IRequestHandler<CancelReservationRequestDto, CancelReservationResponse>
    {
        private readonly IReservationService _reservationService;

        public CancelReservationCommandHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<CancelReservationResponse> Handle(CancelReservationRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _reservationService.CancelReservation(request.ReservationId);

            return response;
        }
    }
}