using FluentValidation;
using HotelManagement.Api.Requests;

namespace HotelManagement.Api.Application.Validations
{
    public class RoomAvailabilityCheckRequestValidator : AbstractValidator<RoomAvailabilityCheckRequest>
    {
        public RoomAvailabilityCheckRequestValidator()
        {
            RuleFor(request => request.RoomTypeIds).NotNull().NotEmpty();
            RuleFor(request => request.RequestedRoomCount).NotNull().GreaterThan(0);
        }
    }
}
