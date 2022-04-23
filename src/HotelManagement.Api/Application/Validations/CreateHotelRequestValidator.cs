using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class CreateHotelRequestValidator : AbstractValidator<CreateHotelRequest>
    {
        public CreateHotelRequestValidator()
        {
            RuleFor(request => request.Hotel).NotNull();
        }
    }
}