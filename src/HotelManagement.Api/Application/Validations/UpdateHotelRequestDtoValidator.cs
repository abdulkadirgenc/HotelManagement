using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class UpdateHotelRequestDtoValidator : AbstractValidator<UpdateHotelRequestDto>
    {
        public UpdateHotelRequestDtoValidator()
        {
            RuleFor(request => request.Hotel).NotNull();
        }
    }
}