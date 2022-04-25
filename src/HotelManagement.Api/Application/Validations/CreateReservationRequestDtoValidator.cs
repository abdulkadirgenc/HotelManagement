using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class CreateReservationRequestDtoValidator : AbstractValidator<CreateReservationRequestDto>
    {
        public CreateReservationRequestDtoValidator()
        {
            RuleFor(request => request.CreateReservation).NotNull();
            RuleFor(request => request.CreateReservation.HotelId).NotNull();
            RuleFor(request => request.CreateReservation.RoomTypeId).NotNull();
            RuleFor(request => request.CreateReservation.RequestedRoomCount).NotNull().GreaterThan(0);
            RuleFor(request => request.CreateReservation.BookingDateStart).NotNull();
            RuleFor(request => request.CreateReservation.BookingDateEnd).NotNull();
            RuleFor(request => request.CreateReservation).Must(cr => cr.BookingDateEnd >= cr.BookingDateStart);
        }
    }
}