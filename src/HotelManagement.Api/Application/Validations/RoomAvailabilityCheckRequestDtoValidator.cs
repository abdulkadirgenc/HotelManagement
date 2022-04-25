using FluentValidation;
using HotelManagement.Api.Requests;

namespace HotelManagement.Api.Application.Validations
{
    public class RoomAvailabilityCheckRequestDtoValidator : AbstractValidator<RoomAvailabilityCheckRequestDto>
    {
        public RoomAvailabilityCheckRequestDtoValidator()
        {
            RuleFor(request => request.Args).NotNull();
            RuleFor(request => request.Args.RoomTypeIds).NotNull().NotEmpty();
            RuleFor(request => request.Args.RequestedRoomCount).NotNull().GreaterThan(0);
        }
    }
}
