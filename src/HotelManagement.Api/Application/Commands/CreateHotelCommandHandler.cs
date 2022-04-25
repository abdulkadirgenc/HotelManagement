using HotelManagement.Api.Requests;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models.Entity;
using MediatR;

namespace HotelManagement.Api.Application.Commands
{
    public class CreateHotelCommandHandler
        : IRequestHandler<CreateHotelRequestDto, HotelModel>
    {
        private readonly IHotelService _hotelService;

        public CreateHotelCommandHandler(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<HotelModel> Handle(CreateHotelRequestDto request, CancellationToken cancellationToken)
        {
            var hotelModel = await _hotelService.CreateHotel(request.Hotel);

            return hotelModel;
        }
    }
}