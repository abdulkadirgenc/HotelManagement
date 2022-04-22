using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Api.Requests;
using HotelManagement.Application.Interfaces;
using MediatR;

namespace HotelManagement.Api.Application.Commands
{
    public class DeleteHotelByIdCommandHandler : IRequestHandler<DeleteHotelByIdRequest>
    {
        private readonly IHotelService _hotelService;

        public DeleteHotelByIdCommandHandler(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<Unit> Handle(DeleteHotelByIdRequest request, CancellationToken cancellationToken)
        {
            await _hotelService.DeleteHotelById(request.Id);

            return Unit.Value;
        }
    }
}
