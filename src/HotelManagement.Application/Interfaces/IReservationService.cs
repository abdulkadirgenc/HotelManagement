using HotelManagement.Application.Models;

namespace HotelManagement.Application.Interfaces
{
    public interface IReservationService
    {
        Task<CreateReservationResponse> CreateReservation(CreateReservationRequest createReservation);
        Task<CancelReservationResponse> CancelReservation(int reservationId);
    }
}
