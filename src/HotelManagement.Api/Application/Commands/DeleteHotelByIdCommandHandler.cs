using HotelManagement.Api.Requests;
using HotelManagement.Application.Interfaces;
using MediatR;

namespace HotelManagement.Api.Application.Commands
{
    public class DeleteHotelByIdCommandHandler : IRequestHandler<DeleteHotelByIdRequestDto>
    {
        private readonly IHotelService _hotelService;

        public DeleteHotelByIdCommandHandler(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<Unit> Handle(DeleteHotelByIdRequestDto request, CancellationToken cancellationToken)
        {
            await _hotelService.DeleteHotelById(request.Id);

            return Unit.Value;
        }
    }
}