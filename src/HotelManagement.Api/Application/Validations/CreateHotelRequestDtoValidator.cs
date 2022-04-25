using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class CreateHotelRequestDtoValidator : AbstractValidator<CreateHotelRequestDto>
    {
        public CreateHotelRequestDtoValidator()
        {
            RuleFor(request => request.Hotel).NotNull();
        }
    }
}