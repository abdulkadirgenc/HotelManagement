using HotelManagement.Api.Requests;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;
using MediatR;

namespace HotelManagement.Api.Application.Commands
{
    public class CreateHotelCommandHandler
        : IRequestHandler<CreateHotelRequest, HotelModel>
    {
        private readonly IHotelService _hotelService;

        public CreateHotelCommandHandler(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<HotelModel> Handle(CreateHotelRequest request, CancellationToken cancellationToken)
        {
            var hotelModel = await _hotelService.CreateHotel(request.Hotel);

            return hotelModel;
        }
    }
}