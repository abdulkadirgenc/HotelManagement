using HotelManagement.Application.Models;
using MediatR;

namespace HotelManagement.Api.Requests
{
    public class CancelReservationRequestDto : IRequest<CancelReservationResponse>
    {
        public int ReservationId { get; set; }
    }
}