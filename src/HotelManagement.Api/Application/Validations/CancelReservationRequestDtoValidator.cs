using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class CancelReservationRequestDtoValidator : AbstractValidator<CancelReservationRequestDto>
    {
        public CancelReservationRequestDtoValidator()
        {
            RuleFor(request => request.ReservationId).NotNull();
        }
    }
}