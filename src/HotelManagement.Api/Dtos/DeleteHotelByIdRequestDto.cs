using MediatR;

namespace HotelManagement.Api.Requests
{
    public class DeleteHotelByIdRequestDto : IRequest
    {
        public int Id { get; set; }
    }
}