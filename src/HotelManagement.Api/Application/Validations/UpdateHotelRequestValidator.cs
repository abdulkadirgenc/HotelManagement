using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class UpdateHotelRequestValidator : AbstractValidator<UpdateHotelRequest>
    {
        public UpdateHotelRequestValidator()
        {
            RuleFor(request => request.Hotel).NotNull();
        }
    }
}
