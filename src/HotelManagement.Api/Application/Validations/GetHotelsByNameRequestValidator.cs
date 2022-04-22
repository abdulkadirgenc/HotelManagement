using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class GetHotelsByNameRequestValidator : AbstractValidator<GetHotelsByNameRequest>
    {
        public GetHotelsByNameRequestValidator()
        {
            RuleFor(request => request.Name).NotNull();
        }
    }
}
