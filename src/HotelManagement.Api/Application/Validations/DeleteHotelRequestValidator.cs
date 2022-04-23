using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class DeleteHotelRequestValidator : AbstractValidator<DeleteHotelByIdRequest>
    {
        public DeleteHotelRequestValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0);
        }
    }
}