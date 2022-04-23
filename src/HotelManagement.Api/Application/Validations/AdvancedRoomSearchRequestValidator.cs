using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class AdvancedRoomSearchRequestValidator : AbstractValidator<AdvancedRoomSearchRequest>
    {
        public AdvancedRoomSearchRequestValidator()
        {
            RuleFor(request => request.RoomTypeIds).NotNull().NotEmpty();
        }
    }
}
