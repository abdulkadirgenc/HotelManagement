using HotelManagement.Application.Models.Entity;
using MediatR;

namespace HotelManagement.Api.Requests
{
    public class CreateHotelRequestDto : IRequest<HotelModel>
    {
        public HotelModel Hotel { get; set; }
    }
}