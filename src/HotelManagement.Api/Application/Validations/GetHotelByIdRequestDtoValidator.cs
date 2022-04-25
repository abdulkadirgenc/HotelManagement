using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class GetHotelByIdRequestDtoValidator : AbstractValidator<GetHotelByIdRequestDto>
    {
        public GetHotelByIdRequestDtoValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0);
        }
    }
}