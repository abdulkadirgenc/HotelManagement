using HotelManagement.Api.Requests;
using HotelManagement.Application.Interfaces;
using MediatR;

namespace HotelManagement.Api.Application.Commands
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelRequest>
    {
        private readonly IHotelService _hotelService;

        public UpdateHotelCommandHandler(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<Unit> Handle(UpdateHotelRequest request, CancellationToken cancellationToken)
        {
            await _hotelService.UpdateHotel(request.Hotel);

            return Unit.Value;
        }
    }
}