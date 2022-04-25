using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class AdvancedRoomSearchRequestDtoValidator : AbstractValidator<AdvancedRoomSearchRequestDto>
    {
        public AdvancedRoomSearchRequestDtoValidator()
        {
            RuleFor(request => request.RoomTypeIds).NotNull().NotEmpty();
        }
    }
}
