using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class DeleteHotelRequestDtoValidator : AbstractValidator<DeleteHotelByIdRequestDto>
    {
        public DeleteHotelRequestDtoValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0);
        }
    }
}