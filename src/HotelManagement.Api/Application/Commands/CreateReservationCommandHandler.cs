using HotelManagement.Api.Requests;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;
using MediatR;

namespace HotelManagement.Api.Application.Commands
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationRequestDto, CreateReservationResponse>
    {
        private readonly IReservationService _reservationService;

        public CreateReservationCommandHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<CreateReservationResponse> Handle(CreateReservationRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _reservationService.CreateReservation(request.CreateReservation);

            return response;
        }
    }
}