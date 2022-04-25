using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class GetHotelsByNameRequestDtoValidator : AbstractValidator<GetHotelsByNameRequestDto>
    {
        public GetHotelsByNameRequestDtoValidator()
        {
            RuleFor(request => request.Name).NotNull();
        }
    }
}