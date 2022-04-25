using HotelManagement.Application.Models;
using HotelManagement.Application.Models.Entity;

namespace HotelManagement.Application.Interfaces
{
    public interface IReservationService
    {
        Task<CreateReservationResponse> CreateReservation(CreateReservationRequest createReservation);
    }
}
