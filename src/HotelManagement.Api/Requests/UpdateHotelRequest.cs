using HotelManagement.Application.Models;
using MediatR;

namespace HotelManagement.Api.Requests
{
    public class UpdateHotelRequest : IRequest
    {
        public HotelModel Hotel { get; set; }
    }
}
