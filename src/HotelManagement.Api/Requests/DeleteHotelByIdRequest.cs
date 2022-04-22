using MediatR;

namespace HotelManagement.Api.Requests
{
    public class DeleteHotelByIdRequest : IRequest
    {
        public int Id { get; set; }
    }
}
