using HotelManagement.Application.Models;
using MediatR;

namespace HotelManagement.Api.Requests
{
    public class CreateHotelRequest : IRequest<HotelModel>
    {
        public HotelModel Hotel { get; set; }
    }
}