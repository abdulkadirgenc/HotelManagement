using HotelManagement.Application.Models;
using MediatR;

namespace HotelManagement.Api.Requests
{
    public class CreateReservationRequestDto : IRequest<CreateReservationResponse>
    {
        public CreateReservationRequest CreateReservation { get; set; }
    }
}