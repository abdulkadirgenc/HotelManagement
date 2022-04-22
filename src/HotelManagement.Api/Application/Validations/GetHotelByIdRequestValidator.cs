using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class GetHotelByIdRequestValidator : AbstractValidator<GetHotelByIdRequest>
    {
        public GetHotelByIdRequestValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0);
        }
    }
}
